import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { Router } from '@angular/router';
import { Country } from '../../models/country.model';
import { Province } from '../../models/province.model';

@Component({
  selector: 'app-step2',
  templateUrl: './step2.component.html',
  styleUrls: ['./step2.component.css']
})
export class Step2Component implements OnInit {
  locationForm!: FormGroup;
  countries: Country[] = [];
  provinces: Province[] = [];

  constructor(private fb: FormBuilder, private apiService: ApiService, private router: Router) {}

  ngOnInit(): void {
    this.locationForm = this.fb.group({
      countryId: ['', Validators.required],
      provinceId: ['', Validators.required]
    });

    this.apiService.getCountries().subscribe((data: Country[]) => {
      this.countries = data;
    });
  }

  onCountryChange() {
    const countryId = this.locationForm.get('countryId')?.value;
    this.apiService.getProvinces(countryId).subscribe((data: Province[]) => {
      this.provinces = data;
    });
  }

  save() {
    if (this.locationForm.valid) {
      const step1Data = JSON.parse(localStorage.getItem('step1Data') || '{}');
      const finalData = { ...step1Data, ...this.locationForm.value };

      this.apiService.register(finalData).subscribe(response => {
        console.log('Registration successful', response);
        this.router.navigate(['/success']);
      });
    }
  }
}
