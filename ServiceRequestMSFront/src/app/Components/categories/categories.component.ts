import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CategoryDto, CategoryItemDto } from 'src/app/Models';
import { CategoryService } from 'src/app/Services/category.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  categories: CategoryItemDto[] = [];
  selectedCategory: CategoryDto | null = null;
  selectedCategoryId = '';
  isLoadingItems = false;
  itemsErrorMessage = '';
  isLoading = false;
  errorMessage = '';

  constructor(
    private categoryService: CategoryService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe((params) => {
      const categoryId = params.get('categoryId');
      if (categoryId) {
        this.openCategoryById(categoryId);
      }
    });

    this.loadCategories();
  }

  loadCategories(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.categoryService.getAllCategories().subscribe({
      next: (data) => {
        this.categories = data;
        this.isLoading = false;
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = 'Could not load categories. Please check API and CORS settings.';
        console.error('Error fetching categories:', error);
      }
    });
  }

  onCategoryClick(category: CategoryItemDto): void {
    if (this.selectedCategoryId === category.id) {
      this.selectedCategoryId = '';
      this.selectedCategory = null;
      this.isLoadingItems = false;
      this.itemsErrorMessage = '';
      return;
    }

    this.openCategoryById(category.id);
  }

  private openCategoryById(categoryId: string): void {
    if (!categoryId) {
      return;
    }

    this.selectedCategoryId = categoryId;
    this.selectedCategory = null;
    this.isLoadingItems = true;
    this.itemsErrorMessage = '';

    this.categoryService.getCategoryById(categoryId).subscribe({
      next: (data) => {
        this.selectedCategory = data;
        this.isLoadingItems = false;
      },
      error: (error) => {
        this.isLoadingItems = false;
        this.itemsErrorMessage = 'Could not load category items.';
        console.error('Error fetching category items:', error);
      }
    });
  }

  isSelected(categoryId: string): boolean {
    return this.selectedCategoryId === categoryId;
  }

}
