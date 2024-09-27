import {Route, Routes} from "react-router-dom";
import RootPage from "./pages/RootPage/RootPage.tsx";
import HomePage from "./pages/HomePage/HomePage.tsx";
import AuthorsPage from "./pages/AuthorsPage/AuthorsPage.tsx";
import BooksPage from "./pages/BooksPage/BooksPage.tsx";
import LoginForm from "./components/LoginForm/LoginForm.tsx";

export default function App() {
    return (
        <>
            <Routes>
                <Route path='/' element={<RootPage/>}>
                    <Route index element={<HomePage/>}/>
                    <Route path='authors' element={<AuthorsPage/>}/>
                    <Route path='books' element={<BooksPage/>}/>
                    <Route path='login' element={<LoginForm/>}/>

                </Route>
            </Routes>
        </>
    );
}