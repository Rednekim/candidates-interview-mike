import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import { Observable, throwError,  } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { IState} from '../shared/interfaces';
import { environment } from '../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class StateService {
    baseUrl = environment.apiUrl;
    baseStatesUrl = this.baseUrl + 'states'

    constructor(private http: HttpClient) { }
    
    getStates(): Observable<IState[]> {
        return this.http.get<IState[]>(this.baseStatesUrl);
    }
}
