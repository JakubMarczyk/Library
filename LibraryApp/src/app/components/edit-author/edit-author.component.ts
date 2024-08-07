import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Author } from 'src/app/models/author.model';
import { AuthorsService } from 'src/app/services/authors.service';

@Component({
  selector: 'app-edit-author',
  templateUrl: './edit-author.component.html',
  styleUrls: ['./edit-author.component.css']
})
export class EditAuthorComponent implements OnInit{

  id: string | null = "";

  author: Author = {
    author_id: undefined,
    firstName: ''
  };

  constructor(private route: ActivatedRoute,
    private router: Router,
    private authorsService: AuthorsService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params: ParamMap) => {
      if (params.get('id')) {
        this.id = params.get('id');
        if (this.id) {
          this.authorsService.getAuthor(this.id)
            .subscribe({
              next: (author) => {
                this.author = author;
              }
            })
        }
      }
    })
  }

  editAuthor() {
    this.authorsService.editAuthor(this.author).subscribe({
      next: () => {
        this.router.navigate(['/authors']);
      },
      error: (response => {
        console.log(response);
      })
    });
  }
}
