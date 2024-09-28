//import classes from './AuthorsPage.module.css';
import AuthorsService from "../../services/AuthorsService.ts";
import {useEffect, useState} from "react";
import IAuthor from "../../interfaces/entities/IAuthor.ts";

export default function AuthorsPage() {

    const [authors, setAuthors] = useState<IAuthor[]>([]);


    const fetchAuthors = async () => {
        try {
            const response = await AuthorsService.getList();

            setAuthors(response.data.data);
        }catch(error) {
            console.log(error);
        }
    }

    useEffect(() => {
        fetchAuthors();
    }, []);

    return (
        <div>
            <h1>Authors</h1>

            <ul>
                {authors.map(author => (<li key={author.authorId}>
                   First Name : {author.firstName}, birth date {author.birthDate}, created at {author.createdAt}, updated at {author.updatedAt}
                </li>))}
            </ul>
        </div>
    );
}