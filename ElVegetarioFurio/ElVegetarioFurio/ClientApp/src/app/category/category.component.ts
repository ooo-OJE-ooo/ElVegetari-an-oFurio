import { Component, OnInit } from '@angular/core';
import { CategoryService } from './category.service';
import { Category } from './category';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  categories: Category[] = [];

  constructor(private categoryService: CategoryService) { }

  ngOnInit() {
    // Wichtig, erst durch subscribe wird die Methode auch aufgerufen (Observer)
    this.categoryService
      .getCategories()
      .subscribe(categories => this.categories = categories);
  }

}
