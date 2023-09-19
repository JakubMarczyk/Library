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
        this.authors.sort((a, b) => Number(a.author_id) - Number(b.author_id));
      },
      error: (response) =>{
        console.log(response);
      }
    })
  }

  deleteAuthor(id:number | undefined) {
    id = Number(id);
    this.authorsService.deleteAuthor(id).subscribe({
      next: () => {
        this.authors.splice(this.authors.findIndex(author => author.author_id === id, 1));
      },
      error: (response) =>{
        console.log(response);
      }
    })
  }

}
