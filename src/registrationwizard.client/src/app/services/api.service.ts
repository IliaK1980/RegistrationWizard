import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Country } from "../models/country.model";
import { Province } from "../models/province.model";

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'api';

  constructor(private http: HttpClient) {}

  getCountries(): Observable<Country[]> {
    return this.http.get<Country[]>(`${this.apiUrl}/country`);
  }

  getProvinces(countryId: number): Observable<Province[]> {
    return this.http.get<Province[]>(`${this.apiUrl}/country/${countryId}/provinces`);
  }

  register(user: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/user/register`, user);
  }
}
