import { Injectable } from "@angular/core"
import { HttpClient } from "@angular/common/http"
import { Observable, catchError, map, switchMap, of } from "rxjs"
import { ToastrService } from "ngx-toastr"
import { Router } from "@angular/router"
import { APP_CONFIG } from "../configs/environment.config"
import { API_ENDPOINTS } from "../constants/api.constants"

@Injectable({
  providedIn: "root",
})
export class AdminService {
  constructor(
    public http: HttpClient,
    public toastr: ToastrService,
    public router: Router,
  ) { }

  apiUrl = APP_CONFIG.apiBaseUrl
  imageUrl = APP_CONFIG.imageBaseUrl

  //User
  userList(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}${API_ENDPOINTS.AdminUser.USER_LIST}`)
  }
  
  


deleteUser(userId: any) {
  // Send user ID as a query parameter, matching backend API
  return this.http.delete(`${this.apiUrl}/User`, {
    params: { id: userId }
  }).pipe(
    catchError((error: any) => {
      // Just log the error to console without showing it to the user
      console.error('Delete user error:', error);
      
      // Get detailed error information
      const errorDetails = {
        status: error.status,
        statusText: error.statusText,
        message: error.message,
        error: error.error
      };
      
      // Log the full error object
      console.error('Full error details:', errorDetails);
      
      // Don't show error to user
      // this.toastr.error(errorMessage);
      
      // Continue without throwing error
      return of({ result: 0, message: 'Failed to delete user' });
    })
  );
}
}