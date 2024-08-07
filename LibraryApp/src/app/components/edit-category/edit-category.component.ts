import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Category } from 'src/app/models/category.model';
import { CategoriesService } from 'src/app/services/categories.service';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit {

  id: string | null = "";

  category: Category = {
    category_id: undefined,
    name: ''
  };

  constructor(private route: ActivatedRoute,
    private router: Router,
    private categoriesService: CategoriesService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params: ParamMap) => {
      if (params.get('id')) {
        this.id = params.get('id');
        if (this.id) {
          this.categoriesService.getCategory(this.id)
            .subscribe({
              next: (category) => {
                this.category = category;
              }
            })
        }
      }
    })
  }

  editCategory() {
    this.categoriesService.editCategory(this.category).subscribe({
      next: () => {
        this.router.navigate(['/categories']);
      },
      error: (response => {
        console.log(response);
      })
    });
  }
}
