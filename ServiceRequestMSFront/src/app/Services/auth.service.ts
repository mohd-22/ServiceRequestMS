import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LoginDto } from '../Models';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'Auth';
  private tokenKey = 'serviceRequestToken';

  constructor(private http: HttpClient) { }

  login(request: LoginDto): Observable<string> {
    return this.http.post(`${environment.apiUrl}/${this.apiUrl}/Login`, request, { responseType: 'text' });
  }
  getUserRole(): string | null {
    const token = this.getToken();
    console.log("Token in getUserRole:" + token);
    if (!token){
      console.log("No token found");
      return null;
    } 



    try {
      const decoded: any = jwtDecode(token);
      console.log("HII");
      return decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || decoded['role'] ;
    } catch (error) {
      console.error('Error decoding token', error);
      console.log("Error decoding token");

      return null;
    }
  }


 getUserId(): string | null {
    const token = this.getToken();
    console.log("Token in getUserId:" + token);
    if (!token){
      console.log("No token found");
      return null;
    } 



    try {
      const decoded: any = jwtDecode(token);
      console.log("HII");
      return decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] || decoded['nameidentifier'] ;
    } catch (error) {
      console.error('Error decoding token', error);
      console.log("Error decoding token");

      return null;
    }
  }

  getUserName(): string | null {
    const token = this.getToken();
    if (!token) {
      return null;
    }

    try {
      const decoded: any = jwtDecode(token);
      return decoded['sub'] || decoded['unique_name'] || null;
    } catch (error) {
      console.error('Error decoding token', error);
      return null;
    }
  }

  getUserEmail(): string | null {
    const token = this.getToken();
    if (!token) {
      return null;
    }

    try {
      const decoded: any = jwtDecode(token);
      return decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'] || decoded['email'] || null;
    } catch (error) {
      console.error('Error decoding token', error);
      return null;
    }
  }

  getFullName(): string | null {
    const token = this.getToken();
    if (!token) {
      return null;
    }

    try {
      const decoded: any = jwtDecode(token);
      return decoded['FullName'] || decoded['fullName'] || decoded['name'] || null;
    } catch (error) {
      console.error('Error decoding token', error);
      return null;
    }
  }

  getTokenExpiry(): string | null {
    const token = this.getToken();
    if (!token) {
      return null;
    }

    try {
      const decoded: any = jwtDecode(token);
      if (!decoded['exp']) {
        return null;
      }

      const expiry = new Date(decoded['exp'] * 1000);
      return expiry.toLocaleString();
    } catch (error) {
      console.error('Error decoding token', error);
      return null;
    }
  }




  setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  hasToken(): boolean {
    return !!this.getToken();
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
  }
}