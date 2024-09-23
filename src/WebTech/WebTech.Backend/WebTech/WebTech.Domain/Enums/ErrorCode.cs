namespace WebTech.Domain.Enums;

public enum ErrorCode
{
    UsernameAlreadyExists,
    DataNotSavedToDatabase,
    UserNotFound,
    AuthenticationFailed,
    SessionNotFound,
    RefreshCookieNotFound,
    InvalidRefreshToken,
    FingerprintCookieNotFound,
    UserIdNotValid,
    AuthorAlreadyExists,
    AuthorNotFound,
    AuthorDataIsTheSame,
    BookAlreadyExists,
    BookNotFound,
    Unknown
}