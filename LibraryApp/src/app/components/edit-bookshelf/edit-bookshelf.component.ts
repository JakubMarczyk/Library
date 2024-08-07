import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Bookshelf } from 'src/app/models/bookshelf.model';
import { BookshelvesService } from 'src/app/services/bookshelves.service';

@Component({
  selector: 'app-edit-bookshelf',
  templateUrl: './edit-bookshelf.component.html',
  styleUrls: ['./edit-bookshelf.component.css']
})
export class EditBookshelfComponent {
  id: string | null = "";

  bookshelf: Bookshelf = {
    bookshelf_id: undefined,
    name: '',
    floor: 0
  };

  constructor(private route: ActivatedRoute,
    private router: Router,
    private bookshelvesService: BookshelvesService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params: ParamMap) => {
      if (params.get('id')) {
        this.id = params.get('id');
        if (this.id) {
          this.bookshelvesService.getBookshelf(this.id)
            .subscribe({
              next: (bookshelf) => {
                this.bookshelf = bookshelf;
              }
            })
        }
      }
    })
  }

  editBookshelf() {
    this.bookshelvesService.editBookshelf(this.bookshelf).subscribe({
      next: () => {
        this.router.navigate(['/bookshelves']);
      },
      error: (response => {
        console.log(response);
      })
    });
  }
}
