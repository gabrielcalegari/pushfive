import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { DragDropModule} from '@angular/cdk/drag-drop';
import { FormsModule } from '@angular/forms';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { CatalogComponent } from './components/catalog/catalog.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { VotingComponent } from './components/voting/voting.component';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { SuccessComponent } from './components/success/success.component';

@NgModule({
   declarations: [
      AppComponent,
      NavbarComponent,
      FooterComponent,
      HomeComponent,
      CatalogComponent,
      VotingComponent,
      SuccessComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      AppRoutingModule,
      BrowserAnimationsModule,
      PaginationModule.forRoot(),
      ToastrModule.forRoot(),
      DragDropModule,
      FormsModule,
      RouterModule.forRoot([
        { path: '', component: HomeComponent },
        { path: 'success', component: SuccessComponent }
      ])
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
