import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/models/category.model';
import { CategoriesService } from 'src/app/services/categories.service';
import { faPenToSquare} from '@fortawesome/free-solid-svg-icons';
import { faTrash} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit{
  categories: Category[] = [];
  faPen = faPenToSquare;
  faTrash = faTrash;
  constructor(private categoriesService: CategoriesService){}

  ngOnInit(): void {
    this.getCategories();
  }

  getCategories() {
    this.categoriesService.getCategories().subscribe({
      next: (categories) => {
        this.categories=categories;
      },
      error: (response) =>{
        console.log(response);
      }
    })
  }

  deleteCategory(id: string | undefined) {
    if (!id) {
      id = "";
    }
    this.categoriesService.deleteCategory(id).subscribe({
      next: () => {
        this.categories = this.categories.filter(category => category.category_id !== id);
      },
      error: (response) =>{
        console.log(response);
      }
    })
  }
}
