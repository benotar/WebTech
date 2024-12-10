import MyHeader from "../../components/Header/MyHeader.tsx";
import {Outlet} from "react-router-dom";
import {FC} from "react";
import { Layout } from 'antd';
import classes from './RootPage.module.css';

const { Content } = Layout;


const RootPage: FC = () => {
    return (
        <Layout className={classes.antLayout}>
            <MyHeader/>
            <Content className={classes.antLayoutContent}>
                <Outlet/>
            </Content>
        </Layout>
    );
}

export default RootPage;