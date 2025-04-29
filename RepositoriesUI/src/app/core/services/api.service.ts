import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GitHubRepository, GitHubSearchResult } from '../../shared/models/GitHubRepository';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private baseUrl = environment.SERVER_BASE_API_URL;

  private http = inject(HttpClient);

  login(username: string, password: string): Observable<string> {
    return this.http.post<string>(`${this.baseUrl}Auth/login`, { username, password });
  }

  searchRepositories(query: string, page: number, perPage: number): Observable<GitHubSearchResult> {
    const url = `${this.baseUrl}Search?query=${query}&page=${page}&per_page=${perPage}`;
    return this.http.get<GitHubSearchResult>(url);
  }

  addBookmark(favorite: GitHubRepository): Observable<GitHubRepository> {
    return this.http.post<GitHubRepository>(`${this.baseUrl}bookmarks`, favorite);
  }

  getBookmarks(): Observable<GitHubRepository[]> {
    return this.http.get<GitHubRepository[]>(`${this.baseUrl}Bookmarks`);
  }

}
