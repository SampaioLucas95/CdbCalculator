import { Directive, HostListener, ElementRef } from '@angular/core';

@Directive({
  selector: '[appCurrencyMask]'
})
export class CurrencyMaskDirective {

  constructor(private el: ElementRef<HTMLInputElement>) { }

  @HostListener('input', ['$event.target.value'])
  onInput(value: string) {
    const numericValue = value.replace(/\D/g, ''); // Remove tudo que não é dígito
    const transformedValue = this.formatToCurrency(numericValue);
    this.el.nativeElement.value = transformedValue;
  }

  private formatToCurrency(value: string): string {
    if (!value) return '';
    const numericValue = parseFloat(value) / 100; // Converter para número e ajustar casas decimais
    return this.formatNumberToCurrency(numericValue);
  }

  private formatNumberToCurrency(value: number): string {
    const formattedValue = value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
    return formattedValue.replace(/\s/g, ''); // Remove espaços em branco, se houver
  }

}
