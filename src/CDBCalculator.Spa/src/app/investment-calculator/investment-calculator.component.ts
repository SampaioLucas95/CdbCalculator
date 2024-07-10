import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CalculateInvestmentService } from './investment-calculator.service';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-calculate-investment',
  templateUrl: './investment-calculator.component.html',
  styleUrls: ['./investment-calculator.component.scss'],
})
export class InvestmentCalculatorComponent {
  formularioCdb: FormGroup;
  valorBruto: number = 0;
  valorLiquido: number = 0;
  private destroy$ = new Subject<void>();

  constructor(
    private fb: FormBuilder,
    private investmentService: CalculateInvestmentService,
    private toastr: ToastrService
  ) {
    this.formularioCdb = this.fb.group({
      valorInicial: [0, [Validators.required, Validators.min(0.01)]],
      meses: [1, [Validators.required, Validators.min(2)]]
    });
  }

  ngOnInit(): void {

  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  showSuccess(message: string) {
    this.toastr.success(message, 'Sucesso');
  }

  showError(message: string) {
    this.toastr.error(message, 'Erro');
  }

  showWarning(message: string) {
    this.toastr.warning(`Atenção! ${message}`, 'Atenção');
  }

  onSubmit(): void {
    if (this.formularioCdb.valid) {
      const valorInicialInformado = this.formularioCdb.value.valorInicial.replace(/\D/g, ''); 
      const valorInicialDecimal = parseFloat(valorInicialInformado) / 100;

      this.investmentService.calculateInvestment(valorInicialDecimal, this.formularioCdb.value.meses)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: (result) => {
            console.log('result:', result);
            if (result?.success) {
              this.valorBruto = result.data.valorBruto;
              this.valorLiquido = result.data.valorLiquido;
              this.showSuccess('Cálculo realizado com sucesso');
            } else {
              this.showWarning(result?.data?.errors.join('\n'));
            }
          },
          error: (error) => {
            console.log('error', error);
            this.showError(error.error.errors);
          }
        });
    }
  }
}
