import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { AppRoutingModule } from './app-routing.module';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptorService } from './auth/auth-interceptor.service';
import { GroupComponent } from './groups/group.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TodosListComponent } from './todos/todos-list/todos-list.component';
import { FilterTodosPipe } from './todos/todos-list/filter-todos.pipe';
import { TodoItemComponent } from './todos/todos-list/todo-item/todo-item.component';
import { TodoItemAddModalComponent } from './todos/todos-list/todo-item-add-modal/todo-item-add-modal.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DragDropModule } from '@angular/cdk/drag-drop';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    GroupComponent,
    HomeComponent,
    LoginComponent,
    SignupComponent,
    TodosListComponent,
    FilterTodosPipe,
    TodoItemComponent,
    TodoItemAddModalComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    NgbModule,
    BrowserAnimationsModule,
    DragDropModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
  entryComponents: [TodoItemAddModalComponent],
})
export class AppModule {}
