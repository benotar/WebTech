// Define sub-paths for the API endpoints
const SUB_PATHS = {
    USERS: 'users',
    AUTHORS: 'authors',
    BOOKS: 'books',
    TOKEN: 'token'
}

// Define sub-paths for the API endpoints
const ACTIONS = {
    GET_ALL: 'get-all',
    CREATE: 'create',
    UPDATE: 'update',
    DELETE: 'delete',
    GET_ME: 'me',
    LOGIN: 'login',
    LOGOUT: 'logout',
    REGISTER: 'register',
    REFRESH: 'refresh'
};

// Define sub-paths for the API endpoints
const PORT: number = 5000;

// Construct the base URLs
export const LOCAL_COM_API_URL: string = `http://bg-local.com:${PORT}`;
export const LOCAL_NET_API_URL: string = `http://api.bg-local.net:${PORT}`;

// Function to build a complete URL based on base URL, sub-path, action, and optional ID
const buildUrl = (base: string, subPath: string, action: string, id: string = '') => {
    return `${base}/${subPath}/${action}${id ? `/${id}` : ''}`;
}

// Define the endpoints for the API
export const ENDPOINTS = {
    USERS: {
        REGISTER: buildUrl(LOCAL_COM_API_URL, SUB_PATHS.USERS, ACTIONS.REGISTER),
        LOGIN: buildUrl(LOCAL_COM_API_URL, SUB_PATHS.USERS, ACTIONS.LOGIN),
        LOGOUT: buildUrl(LOCAL_COM_API_URL, SUB_PATHS.USERS, ACTIONS.LOGOUT),
        ME: buildUrl(LOCAL_COM_API_URL, SUB_PATHS.USERS, ACTIONS.GET_ME),
    },
    REFRESH: buildUrl(LOCAL_COM_API_URL, SUB_PATHS.TOKEN, ACTIONS.REFRESH),
    AUTHORS: {
        GET: buildUrl(LOCAL_NET_API_URL, SUB_PATHS.AUTHORS, ACTIONS.GET_ALL),
        CREATE: buildUrl(LOCAL_NET_API_URL, SUB_PATHS.AUTHORS, ACTIONS.CREATE),
        UPDATE: (authorId: string): string => buildUrl(LOCAL_NET_API_URL, SUB_PATHS.AUTHORS, ACTIONS.UPDATE, authorId),
        DELETE: (authorId: string): string => buildUrl(LOCAL_NET_API_URL, SUB_PATHS.AUTHORS, ACTIONS.DELETE, authorId)
    },
    BOOKS: {
        GET: buildUrl(LOCAL_NET_API_URL, SUB_PATHS.BOOKS, ACTIONS.GET_ALL),
        CREATE: buildUrl(LOCAL_NET_API_URL, SUB_PATHS.BOOKS, ACTIONS.CREATE),
        UPDATE: (bookId: string): string => buildUrl(LOCAL_NET_API_URL, SUB_PATHS.BOOKS, ACTIONS.UPDATE, bookId),
        DELETE: (bookId: string): string => buildUrl(LOCAL_NET_API_URL, SUB_PATHS.BOOKS, ACTIONS.DELETE, bookId)
    },
}
