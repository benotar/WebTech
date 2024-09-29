import {Route, Routes} from "react-router-dom";
import RootPage from "./pages/RootPage/RootPage.tsx";
import HomePage from "./pages/HomePage/HomePage.tsx";
import AuthorsPage from "./pages/AuthorsPage/AuthorsPage.tsx";
import BooksPage from "./pages/BooksPage/BooksPage.tsx";
import LoginPage from "./pages/LoginPage/LoginPage.tsx";
import Logout from "./components/Logout/Logout.tsx";
import UsersPage from "./pages/UsersPage/UsersPage.tsx";
import RegisterPage from "./pages/RegisterPage/RegisterPage.tsx";
import ErrorPage from "./pages/ErrorPage/ErrorPage.tsx";

export default function App() {
    return (
        <>
            <Routes>
                <Route path='/' element={<RootPage/>}>
                    <Route index element={<HomePage/>}/>
                    <Route path='authors' element={<AuthorsPage/>}/>
                    <Route path='books' element={<BooksPage/>}/>
                    <Route path='login' element={<LoginPage/>}/>
                    <Route path='register' element={<RegisterPage/>}/>
                    <Route path='logout' element={<Logout/>}/>
                    <Route path='me' element={<UsersPage/>}/>
                    <Route path='*' element={<ErrorPage/>}/>
                </Route>
            </Routes>
        </>
    );
}