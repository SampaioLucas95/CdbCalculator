import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InvestmentCalculatorComponent } from './investment-calculator.component';

const routes: Routes = [
  { path: 'calculate-investment', component: InvestmentCalculatorComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InvestmentCalculatorRoutingModule { }
