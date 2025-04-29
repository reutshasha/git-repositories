export interface GitHubSearchResult {
  total_count: number;
  items: GitHubRepository[];
}

// export interface GitHubRepository {
//   id: number;
//   name?: string;
//   description?: string;
//   stargazers_count?: number;
//   html_url?: string;
// }



// export interface SearchResponse {
//   statusCode: number;
//   message: string;
//   data?: SearchData[]; // '?' מציין שזה אופציונלי
// }

// export interface SearchData {
//   total_count: number;
//   items: any[]; // או GitHubRepository[] אם ה-API מחזיר כבר את המבנה הזה
// }
export interface GitHubRepository {
  id: number;
  name: string;
  owner?: GitHubOwner;
}

export interface GitHubOwner {
  avatar_url?: string;
}

