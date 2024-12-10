import classes from './ErrorPage.module.css';
import {Result, Button} from "antd";
import {Link} from "react-router-dom";
import {FC} from "react";

const ErrorPage: FC = () => {
    return (
        <div className={classes.resultContainer}>
            <Result
                status="404"
                title="Oops! Looks like you're lost."
                subTitle="Check the URL for errors or try refreshing the page or back to home page."
                extra={
                    <Link to="/">
                        <Button type="primary" className={classes.buttonStyle}>
                            Back to the home page
                        </Button>
                    </Link>
                }
            />
        </div>
    );
}

export default ErrorPage;