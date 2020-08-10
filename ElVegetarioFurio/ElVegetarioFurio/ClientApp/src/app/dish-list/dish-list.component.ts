import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CategoryService } from '../category/category.service';
import { Category } from '../category/category';

@Component({
  selector: 'app-dish-list',
  templateUrl: './dish-list.component.html',
  styleUrls: ['./dish-list.component.css']
})
export class DishListComponent implements OnInit {

  constructor(private route: ActivatedRoute, private categoryService: CategoryService) { }
  category: Category; 
  ngOnInit() {
    // this.categoryId ist ein  String (error) - Lösung vor das zweite this ein + schreiben dann wird daraus eine Zahl gemacht 
    const categoryId = +this.route.snapshot.paramMap.get('categoryId');
    this.categoryService
      .getCategory(categoryId)
      .subscribe(category => this.category = category);
  }

}
