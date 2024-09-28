//import classes from './AuthorsPage.module.css';
import {useEffect} from "react";
import {useAuthorsStore} from "../../stores/useAuthorsStore.ts";

export default function AuthorsPage() {

    const authorsStore = useAuthorsStore();

    const fetchAuthors = async () => {
        await authorsStore.getAuthors();
    }

    useEffect(() => {
        fetchAuthors();
    }, []);

    return (
        <div>
            <h1>Authors</h1>

            {/*<ul>*/}
            {/*    {authorsStore.authors.map(author => (*/}
            {/*        <li key={author.id}>*/}
            {/*            First Name : {author.firstName}, lastName: {author.lastName} | birth date {author.birthDate}, created*/}
            {/*            at {author.createdAt},*/}
            {/*            updated at {author.updatedAt}*/}
            {/*        </li>))}*/}
            {/*</ul>*/}
        </div>
    );
}