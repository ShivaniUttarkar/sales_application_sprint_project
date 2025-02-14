import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { EmployeeRegisterRequest } from '../Models/employee-register-request';
import { LoginRequest } from '../Models/login-request';
import { LoginResponse } from '../Models/login-response';
import { ShipperRegisterRequest } from '../Models/shipper-register-request';
import { RegisterResponse } from '../Models/register-response';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl:string = "https://localhost:7127/api/auth";
  private userPayload:any;

  constructor(private http:HttpClient,private router:Router) { 
    this.userPayload = this.decodedToken();
  }

  login(request : LoginRequest) : Observable<LoginResponse>{
    console.log(request);
    return this.http.post<LoginResponse>(`${this.baseUrl}/login`,request);
  }

  signupShipper(request : ShipperRegisterRequest){
    console.log(request);
    return this.http.post<RegisterResponse>(`${this.baseUrl}/shipper-register`,request)
  }

  signupEmployee(request: FormData){
    console.log(request);
    return this.http.post<RegisterResponse>(`${this.baseUrl}/employee-register`,request);
  }

  signupAdmin(request: any){
    console.log(request);
    return this.http.post<RegisterResponse>(`${this.baseUrl}/admin-register`,request);
  }

  storeToken(token:string)
  {
    localStorage.setItem("token",token);
  }

  logout(){
    localStorage.removeItem("token");
    this.router.navigate(["login"]);
  }

  getToken()
  {
    return localStorage.getItem("token");
  }

  isUserLoggedIn():boolean{
    return !!localStorage.getItem("token");
  }

  decodedToken(){
    const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    return jwtHelper.decodeToken(token);
  }

  getRoleFromToken(){
    if(this.userPayload)
      return this.userPayload.role;
  }

  getEmailFromToken(){
    if(this.userPayload)
      return this.userPayload.email;
  }

  getUserFromToken(){
    if(this.userPayload){
      return this.userPayload.JSON
    }
  }
}

