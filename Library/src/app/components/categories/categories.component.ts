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
        this.categories.sort((a,b) => Number(a.category_id) - Number(b.category_id))
      },
      error: (response) =>{
        console.log(response);
      }
    })
  }

  deleteCategory(id:number | undefined) {
    id = Number(id);
    this.categoriesService.deleteCategory(id).subscribe({
      next: () => {
        this.categories.splice(this.categories.findIndex(category => category.category_id === id),1);
      },
      error: (response) =>{
        console.log(response);
      }
    })
  }
}
