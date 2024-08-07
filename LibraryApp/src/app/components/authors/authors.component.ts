import { Component, OnInit } from '@angular/core';
import { Author } from 'src/app/models/author.model';
import { AuthorsService } from 'src/app/services/authors.service';
import { faPenToSquare} from '@fortawesome/free-solid-svg-icons';
import { faTrash} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.css']
})

export class AuthorsComponent implements OnInit{
  authors: Author[] = [];

  constructor(private authorsService: AuthorsService) { }

  faPen = faPenToSquare;
  faTrash = faTrash;

  ngOnInit(): void {
    this.getAuthors();;

  }

  getAuthors() {
    this.authorsService.getAuthors().subscribe({
      next: (authors) => {
        this.authors=authors;
      },
      error: (response) =>{
        console.log(response);
      }
    })
  }

  deleteAuthor(id: string | undefined) {
    if (!id) {
      id = "";
    }
    this.authorsService.deleteAuthor(id).subscribe({
      next: () => {
        this.authors = this.authors.filter(author => author.author_id !== id);
      },
      error: (response) =>{
        console.log(response);
      }
    })
  }

}
