export const LOCAL_COM_API_URL: string = 'http://bg-local.com:5000';
export const LOCAL_NET_API_URL: string = 'http://api.bg-local.net:5000';

export const ENDPOINTS = {
    USERS: {
        REGISTER: `${LOCAL_COM_API_URL}/register`,
        LOGIN: `${LOCAL_COM_API_URL}/login`,
        LOGOUT: `${LOCAL_COM_API_URL}/logout`,
        ME: `${LOCAL_COM_API_URL}/me`,
    },
    REFRESH:  `${LOCAL_COM_API_URL}/refresh`,
    AUTHORS: {
        GET: `${LOCAL_NET_API_URL}/authors/get-all`,
        CREATE: `${LOCAL_NET_API_URL}/authors/create`,
        UPDATE: (authorId: string) => `${LOCAL_NET_API_URL}/authors/update/${authorId}`,
        DELETE: (authorId: string) => `${LOCAL_NET_API_URL}/authors/delete/${authorId}`
    },
    BOOKS: {
        GET: `${LOCAL_NET_API_URL}/books/get-all`,
        CREATE: `${LOCAL_NET_API_URL}/books/create`,
        UPDATE: (bookId: string) => `${LOCAL_NET_API_URL}/books/update/${bookId}`,
        DELETE: (bookId: string) => `${LOCAL_NET_API_URL}/books/delete/${bookId}`
    },

}