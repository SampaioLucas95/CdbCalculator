import { NgModule } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToastrModule } from 'ngx-toastr';

import localePt from '@angular/common/locales/pt';

registerLocaleData(localePt);

import { AppComponent } from './app.component';
import { InvestmentCalculatorModule } from './investment-calculator/investment-calculator.module'; 
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'cdb',
    loadChildren: () => import('./investment-calculator/investment-calculator.module').then(m => m.InvestmentCalculatorModule)
  },
];

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
      closeButton: true
    }),
    InvestmentCalculatorModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
