import MyHeader from "../../components/Header/MyHeader.tsx";
import {Outlet} from "react-router-dom";



export default function RootPage() {
    return (
        <>
            <MyHeader/>
            <main><Outlet/></main>
        </>
    );
}