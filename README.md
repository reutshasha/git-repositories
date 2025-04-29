**GitHub Repository Search API with JWT Authentication**

This project provides a API for searching GitHub repositories, 
secured with JWT (JSON Web Token) authentication. User-specific bookmarks are stored server-side within the session, linked to their JWT UserID. 
The front-end is built with Angular, offering login, repository search with pagination, and a display of bookmarked repositories.

**Key Features:**

•	JWT Authentication: All API endpoints, except for login, require a valid JWT token for access. Tokens are issued upon successful login and have a 20-minute expiration period.

•	Secure Authorization: The API employs JWT Bearer token-based authorization, ensuring only authenticated requests are processed.

•	Repository Search: The GET /api/Search endpoint allows authenticated users to search for GitHub repositories based on a query, with results likely supporting pagination (though not explicitly shown in the provided Swagger UI snippet).

Add to Bookmarks: Authenticated users can bookmark repositories via the /api/bookmarks endpoint.

•	View Bookmarks: The /api/bookmarks endpoint retrieves a list of the user's bookmarked repositories.

•	Angular Frontend: A user-friendly Angular application provides: 

o	/login: A dedicated page for user authentication and JWT token acquisition.

o	HTTP Interceptor: An Angular HTTP interceptor automatically attaches the JWT token to all outgoing API requests (excluding login).

o	/repository-search: A page for searching GitHub repositories, displaying results with pagination for efficient browsing.

o	"Add to Bookmark" Button: A button on each search result to allow users to bookmark repositories.

o	/bookmarks: A page displaying the user's currently bookmarked repositories.

o Global Authorization: The project utilizes global [Authorize] filters, ensuring that all endpoints, except login, are protected by default, making the application more secure.

•	Swagger Integration: The API's endpoints and data structures are documented using Swagger, facilitating easy exploration and testing.

**How to Use:**

**1.**	Login and Get Token (Angular Frontend - /login):
o	Navigate to the /login page in the Angular application.
o	Provide your username and password in the login form.

**"username": "testuser", "password": "Pass1357!"**

o	Upon successful authentication, the API will return a JWT token. This token is typically stored in the browser's local storage or session storage by the Angular application.
o	Important: The obtained JWT token expires after 20 minutes.

**2.**
	Accessing Protected Endpoints (Angular Frontend - Automatic via Interceptor):
o	For all subsequent API requests made from the Angular application (except /api/auth/login), the Angular HTTP interceptor will automatically include the JWT token in the Authorization header as a Bearer token.
o	Header: Authorization: Bearer <jwt_token>

**3.**	
Searching Repositories (Angular Frontend - /repository-search):

o	Navigate to the /repository-search page in the Angular application.

o	Enter your search query in the provided input field.

o	Click the "Search" button or press Enter.

o	The Angular application will make a GET request to /api/github/repositories (with the JWT token in the header) to retrieve the search results.

o	Results will be displayed with pagination controls to navigate through multiple pages of results.

**4**.	
Adding to Bookmarks (Angular Frontend - /repository-search):
o	On each repository displayed in the search results, there will be an "Add to Bookmark" button.

o	Clicking this button will trigger a POST request from the Angular application to the /api/bookmarks endpoint, including the repository details in the request body (and the JWT token in the header).

5.	Viewing Bookmarks (Angular Frontend - /bookmarks):

o	Navigate to the /bookmarks page in the Angular application.

o	The Angular application will make a GET request to the /api/bookmarks endpoint (with the JWT token in the header) to fetch and display the user's bookmarked repositories.
