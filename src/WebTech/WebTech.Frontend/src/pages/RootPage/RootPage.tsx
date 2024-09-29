import MyHeader from "../../components/Header/MyHeader.tsx";
import {Outlet} from "react-router-dom";
import {FC} from "react";


const RootPage: FC = () => {
    return (
        <>
            <MyHeader/>
            <main><Outlet/></main>
        </>
    );
}

export default RootPage;