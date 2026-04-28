import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CategoryDto, CategoryItemDto, CreateCategoryDto, ItemDto, UpdateCategoryDto } from 'src/app/Models';
import { CategoryService } from 'src/app/Services/category.service';
import { AuthService } from 'src/app/Services/auth.service';
import { ItemService } from 'src/app/Services/item.service';

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
  isAddingCategory = false;
  isSubmittingAddCategory = false;
  addCategoryErrorMessage = '';
  newCategory: CreateCategoryDto = this.createEmptyCategory();
  role: string | null = null;
  isAddingItem = false;
  isSubmittingAddItem = false;
  addItemErrorMessage = '';
  newItem: ItemDto = this.createEmptyItem();
  isEditingCategory = false;
  isSubmittingEditCategory = false;
  editCategoryErrorMessage = '';
  editingCategory: UpdateCategoryDto = this.createEmptyUpdateCategory();
  isEditingItem = false;
  isSubmittingEditItem = false;
  editItemErrorMessage = '';
  editingItem: UpdateCategoryDto = this.createEmptyUpdateCategory();

  constructor(
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private authService: AuthService,
    private itemService: ItemService
  ) { }

  ngOnInit(): void {
    this.role = this.authService.getUserRole();

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

  canAddCategory(): boolean {
    return this.role === 'Admin';
  }

  canAddItem(): boolean {
    return this.role === 'Admin';
  }

  openAddCategoryForm(): void {
    if (!this.canAddCategory()) {
      return;
    }

    this.addCategoryErrorMessage = '';
    this.newCategory = this.createEmptyCategory();
    this.isAddingCategory = true;
  }

  closeAddCategoryForm(): void {
    this.isAddingCategory = false;
    this.isSubmittingAddCategory = false;
    this.addCategoryErrorMessage = '';
    this.newCategory = this.createEmptyCategory();
  }

  submitAddCategory(): void {
    if (!this.canAddCategory() || this.isSubmittingAddCategory) {
      return;
    }

    this.isSubmittingAddCategory = true;
    this.addCategoryErrorMessage = '';

    this.categoryService.createCategory(this.newCategory).subscribe({
      next: () => {
        this.isSubmittingAddCategory = false;
        this.closeAddCategoryForm();
        this.loadCategories();
      },
      error: (error) => {
        this.isSubmittingAddCategory = false;
        this.addCategoryErrorMessage = 'Could not add category. Please check input values.';
        console.error('Error adding category:', error);
      }
    });
  }

  openAddItemForm(): void {
    if (!this.canAddItem() || !this.selectedCategoryId) {
      return;
    }

    this.addItemErrorMessage = '';
    this.newItem = this.createEmptyItem();
    this.newItem.categoryId = this.selectedCategoryId;
    this.isAddingItem = true;
  }

  closeAddItemForm(): void {
    this.isAddingItem = false;
    this.isSubmittingAddItem = false;
    this.addItemErrorMessage = '';
    this.newItem = this.createEmptyItem();
  }

  submitAddItem(): void {
    if (!this.canAddItem() || this.isSubmittingAddItem) {
      return;
    }

    this.isSubmittingAddItem = true;
    this.addItemErrorMessage = '';

    this.itemService.createItem(this.newItem).subscribe({
      next: () => {
        this.isSubmittingAddItem = false;
        this.closeAddItemForm();
        // Reload the selected category to show the new item
        this.openCategoryById(this.selectedCategoryId);
      },
      error: (error) => {
        this.isSubmittingAddItem = false;
        this.addItemErrorMessage = 'Could not add item. Please check input values.';
        console.error('Error adding item:', error);
      }
    });
  }

  deleteCategory(category: CategoryItemDto): void {
    if (!this.canAddCategory()) {
      return;
    }

    if (!confirm(`Are you sure you want to delete the category "${category.name}"? This action cannot be undone.`)) {
      return;
    }

    this.categoryService.deleteCategory(category.id).subscribe({
      next: () => {
        this.loadCategories();
        if (this.selectedCategoryId === category.id) {
          this.selectedCategoryId = '';
          this.selectedCategory = null;
        }
      },
      error: (error) => {
        this.errorMessage = 'Could not delete category.';
        console.error('Error deleting category:', error);
      }
    });
  }

  deleteItem(item: CategoryItemDto): void {
    if (!this.canAddItem()) {
      return;
    }

    if (!confirm(`Are you sure you want to delete the item "${item.name}"? This action cannot be undone.`)) {
      return;
    }

    this.itemService.deleteItem(item.id).subscribe({
      next: () => {
        // Reload the selected category to update the items list
        this.openCategoryById(this.selectedCategoryId);
      },
      error: (error) => {
        this.itemsErrorMessage = 'Could not delete item.';
        console.error('Error deleting item:', error);
      }
    });
  }

  openEditCategoryForm(category: CategoryItemDto): void {
    if (!this.canAddCategory()) {
      return;
    }

    this.editCategoryErrorMessage = '';
    this.editingCategory = {
      id: category.id,
      name: category.name,
      description: category.description || ''
    };
    this.isEditingCategory = true;
  }

  closeEditCategoryForm(): void {
    this.isEditingCategory = false;
    this.isSubmittingEditCategory = false;
    this.editCategoryErrorMessage = '';
    this.editingCategory = this.createEmptyUpdateCategory();
  }

  submitEditCategory(): void {
    if (!this.canAddCategory() || this.isSubmittingEditCategory) {
      return;
    }

    this.isSubmittingEditCategory = true;
    this.editCategoryErrorMessage = '';

    this.categoryService.updateCategory(this.editingCategory).subscribe({
      next: () => {
        this.isSubmittingEditCategory = false;
        this.closeEditCategoryForm();
        this.loadCategories();
      },
      error: (error) => {
        this.isSubmittingEditCategory = false;
        this.editCategoryErrorMessage = 'Could not update category. Please check input values.';
        console.error('Error updating category:', error);
      }
    });
  }

  openEditItemForm(item: CategoryItemDto): void {
    if (!this.canAddItem()) {
      return;
    }

    this.editItemErrorMessage = '';
    this.editingItem = {
      id: item.id,
      name: item.name,
      description: item.description || ''
    };
    this.isEditingItem = true;
  }

  closeEditItemForm(): void {
    this.isEditingItem = false;
    this.isSubmittingEditItem = false;
    this.editItemErrorMessage = '';
    this.editingItem = this.createEmptyUpdateCategory();
  }

  submitEditItem(): void {
    if (!this.canAddItem() || this.isSubmittingEditItem) {
      return;
    }

    this.isSubmittingEditItem = true;
    this.editItemErrorMessage = '';

    this.itemService.updateItem(this.editingItem).subscribe({
      next: () => {
        this.isSubmittingEditItem = false;
        this.closeEditItemForm();
        // Reload the selected category to show the updated item
        this.openCategoryById(this.selectedCategoryId);
      },
      error: (error) => {
        this.isSubmittingEditItem = false;
        this.editItemErrorMessage = 'Could not update item. Please check input values.';
        console.error('Error updating item:', error);
      }
    });
  }

  private createEmptyUpdateCategory(): UpdateCategoryDto {
    return {
      id: '',
      name: '',
      description: ''
    };
  }

  private createEmptyCategory(): CreateCategoryDto {
    return {
      name: '',
      description: ''
    };
  }

  private createEmptyItem(): ItemDto {
    return {
      name: '',
      description: '',
      categoryId: ''
    };
  }
}
