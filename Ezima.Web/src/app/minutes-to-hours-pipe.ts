import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'minutesToHours',
  standalone: false
})
export class MinutesToHoursPipe implements PipeTransform {

  transform(value: number | undefined): string {
    if (!value) {
      return '0h';
    }
    const hours = value / 60;
    return hours.toFixed(1) + "h"
  }

}
