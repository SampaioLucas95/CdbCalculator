import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field'; // Importe o MatFormFieldModule
import { MatInputModule } from '@angular/material/input';
import { HttpClientModule } from '@angular/common/http';
import { MatButtonModule } from '@angular/material/button';

import { InvestmentCalculatorComponent } from './investment-calculator.component';
import { CalculateInvestmentService } from './investment-calculator.service';


import { InvestmentCalculatorRoutingModule } from './investment-calculator-routing-module';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { CurrencyMaskDirective } from '../shared/directives/currency-mask.directive';



@NgModule({
  declarations: [
    InvestmentCalculatorComponent,
    CurrencyMaskDirective
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    HttpClientModule,
    InvestmentCalculatorRoutingModule
  ],
  providers: [
    provideAnimationsAsync(),
    CalculateInvestmentService
  ],
  exports: [
    InvestmentCalculatorComponent
  ]
})
export class InvestmentCalculatorModule { }
