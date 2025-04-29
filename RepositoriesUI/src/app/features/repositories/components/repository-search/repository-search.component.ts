import { Component, inject } from '@angular/core';
import { GitHubRepository } from '../../../../shared/models/GitHubRepository';
import { ApiService } from '../../../../core/services/api.service';
import { AngularMaterialModule } from '../../../../angular-material.module';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { SnackBarUtil } from '../../../../shared/utilities/snack-bar.util';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-repository-search',
  templateUrl: './repository-search.component.html',
  styleUrl: './repository-search.component.scss',
  standalone: true,
  imports: [AngularMaterialModule, CommonModule, FormsModule, RouterModule],
})
export class RepositorySearchComponent {
  query: string = '';
  results: GitHubRepository[] = [];
  favorites: GitHubRepository[] = [];
  totalResults: number = 0;
  currentPage: number = 1;
  perPage: number = 20;
  isAddingBookmark: { [repoId: number]: boolean } = {};
  isBookmarkedState: { [repoId: number]: boolean } = {};

  private apiService = inject(ApiService);
  private snackBar = inject(SnackBarUtil);

  ngOnInit(): void {
  }

  search() {
    if (!this.query) {
      this.results = [];
      return;
    }
    this.apiService.searchRepositories(this.query, this.currentPage, this.perPage).subscribe({
      next: (response: any) => {
        if (response?.data?.[0]?.items) {
          this.results = response.data[0].items;
        } else {
          this.results = [];
        }
        if (response?.data?.[0]?.total_count !== undefined) {
          this.totalResults = response.data[0].total_count;
        } else {
          this.totalResults = 0;
        }
        this.initializeBookmarkStates();
      },
      error: (error) => {
        console.error('Error fetching repositories:', error);
        this.snackBar.show('Error fetching repositories');
      },
    });
  }

  onPageChange(event: any) {
    this.currentPage = event.pageIndex + 1;
    this.perPage = event.pageSize;
    this.search();
  }

  addBookmark(repository: GitHubRepository) {
    if (!this.isAddingBookmark[repository.id] && !this.isBookmarkedState[repository.id]) {
      this.isAddingBookmark[repository.id] = true;
      this.apiService.addBookmark(repository).pipe(
        finalize(() => this.isAddingBookmark[repository.id] = false)
      ).subscribe({
        next: (response) => {
          this.isBookmarkedState[repository.id] = true;
          this.favorites.push(repository);
          this.snackBar.show('Bookmark added successfully');
        },
        error: (error) => {
          this.snackBar.show('Error adding to bookmarks');
          console.error('Error adding bookmark:', error);
        }
      });
    } else if (this.isBookmarkedState[repository.id]) {
      this.snackBar.show('This repository is already in bookmarks');
    }
  }

  loadFavoritesOnInit() {
    this.apiService.getBookmarks().subscribe({
      next: (favorites) => {
        this.favorites = favorites;
        this.initializeBookmarkStates();
      },
      error: (error) => {
        this.snackBar.show('Error fetching bookmark');
        console.error('Error fetching bookmark:', error);
      }
    });
  }

  initializeBookmarkStates() {
    this.results.forEach(repo => {
      this.isAddingBookmark[repo.id] = false;
      this.isBookmarkedState[repo.id] = this.isAlreadyFavorite(repo);
    });
  }

  isAlreadyFavorite(repository: GitHubRepository): boolean {
    return this.favorites.some((fav) => fav.id === repository.id);
  }

  isBookmarked(repository: GitHubRepository): boolean {
    return this.isBookmarkedState[repository.id];
  }
}






