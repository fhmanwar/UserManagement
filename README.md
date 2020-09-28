# User Management Apps

## [Table of Contents](#)

- [`Usage`](#usage)
    - [`Global API`](#global-api)
    - [`User Management`](#user-management)
    - [`Asset Management`](#asset-management)
    - [`Exam Online`](#exam-online)
    - [`Reimbursement Parking`](#reimbursement-parking)
    - [`Interview & Placement`](#interview-&-placement)
- [SDLC](#sdlc)
    - [ERD Scheme](#ERD-Scheme)
    - [Use Case Diagram Scheme](#use-case-diagram-Scheme)
    - [BPMN Scheme](#bpmn-Scheme)

## Usage

### Global API

1. Using Repository Pattern : 
    - Divisions : 
        - `GET` All : `http://{host}/api/divisions/`
        - `GET` ID : `http://{host}/api/divisions/{id}/`
        - `POST` : `http://{host}/api/divisions/{id}/`
            ```json
                {
                    "Name": "{Division Name}",
                }
            ```
        - `PUT` : `http://{host}/api/divisions/{id}/`
            ```json
                {
                    "Name": "{Division Name}",
                }
            ```
        - `DELETE` : `http://{host}/api/divisions/{id}/`
    
2. Using Dapper :
    - User : 
        - `POST` Users : `http://{host}/api/Users/`
        ```json
            {
                "Password": "{Password}",
            }
        ```

    - Role : 
        - `GET` All Asset Management: `http://{host}/api/users/`
        - `GET` ID Asset Management: `http://{host}/api/users/{id}/`
        - `POST` Reset Password : `http://{host}/api/auths/forgot/`

        ```json
            {
                "Name": "{Role Name}",
                "Session": "{Session who did this method }",
            }
        ```
    


3. Manually :
    - Log Activity : 
        - `GET` All Log Activity : `http://{host}/api/logs/`
        - `POST` Log Activity : `http://{host}/api/logs/`
        ```json
            {
                "Response": "{Like a Message will to send}",
                "Email": "{Email}",
            }
        ```
        
    - Role :             
        - `PUT` : `http://{host}/api/division/{id}/`
            ```json
                {
                    "Name": "{Role Name}",
                    "Session": "{Session who did this method }",
                }
            ```
        - `DELETE` : `http://{host}/api/division/{id}/`


### User Management

- `GET` All User Management: `http://{host}/api/users/`
- `GET` ID User Management: `http://{host}/api/users/{id}`
- `POST` Users : `http://{host}/api/Users/`
    ```json
        {
            "name": "{Name}",
            "nik": "{NIK}",
            "site": "{Assignment Site}",
            "email": "{ Users Email }",
            "password": "{ User Password }",
            "roleName": "{User Role Name}",
            "phone": "{ Phone }",
            "address": "{ User Address }",
            "province": "{ User Province }",
            "city": "{ City }",
            "subDistrict": "{User Sub District}",
            "village": "{ User Village }",
            "zipCode": "{ User Zipcode Address}",
            "Session": "{Session who did this method }",
        }
    ```

- `PUT` Users : `http://{host}/api/Users/`
    ```json
        {
            "name": "{Name}",
            "nik": "{NIK}",
            "site": "{Assignment Site}",
            "email": "{ Users Email }",
            "password": "{ User Password }",
            "roleName": "{User Role Name}",
            "phone": "{ Phone }",
            "address": "{ User Address }",
            "province": "{ User Province }",
            "city": "{ City }",
            "subDistrict": "{User Sub District}",
            "village": "{ User Village }",
            "zipCode": "{ User Zipcode Address}",
            "Session": "{Session who did this method }",
        }
    ```
- `DELETE` : `http://{host}/api/division/{id}/`

- `POST` Forgot Password User Management: `http://{host}/api/auths/forgot`
```json
    {
        "Email": "{Email User}",
    }
```
- `POST` Login Asset Management: `http://{host}/api/auths/login`

```json
    {
        "Email": "{Email User}",
        "Password": "{Password User}"
    }
```

### Asset Management

- `GET` All Asset Management: `http://{host}/api/assetmanages`
- `GET` ID Asset Management: `http://{host}/api/assetmanages/{id}`
- `POST` Forgot Password Asset Management: `http://{host}/api/assetmanages/forgot`

```json
    {
        "Email": "{Email User}",
    }
```
- `POST` Login Asset Management: `http://{host}/api/auths/login`

```json
    {
        "Email": "{Email User}",
        "Password": "{Password User}"
    }
```

### Exam Online

- `GET` All Exam Online: `http://{host}/api/exams/`
- `GET` ID Exam Online: `http://{host}/api/exams/{id}`
- `POST` Forgot Password Exam Online: `http://{host}/api/exams/forgot`

```json
    {
        "Email": "{Email User}",
    }
```
- `POST` Login Exam Online: `http://{host}/api/auths/login`

```json
    {
        "Email": "{Email User}",
        "Password": "{Password User}"
    }
```

### Reimbursement Parking

- `GET` All Reimbursement Parking: `http://{host}/api/reimburs/`
- `GET` ID Reimbursement Parking: `http://{host}/api/reimburs/{id}`
- `POST` Forgot Password Reimbursement Parking: `http://{host}/api/reimburs/forgot`

```json
    {
        "Email": "{Email User}",
    }
```
- `POST` Login Reimbursement Parking: `http://{host}/api/auths/login`

```json
    {
        "Email": "{Email User}",
        "Password": "{Password User}"
    }
```

### Interview & Placement

- `GET` All Interview & Placement: `http://{host}/api/interviews`
- `GET` ID Interview & Placement: `http://{host}/api/interviews/{id}`
- `POST` Forgot Password Interview & Placement: `http://{host}/api/interviews/forgot`

```json
    {
        "Email": "{Email User}",
    }
```
- `POST` Login Interview & Placement: `http://{host}/api/auths/login`

```json
    {
        "Email": "{Email User}",
        "Password": "{Password User}"
    }
```

## SDLC

- `ERD` Scheme

![picture](SDLC/ERD_PostSeminarFeedback.jpg)

- `Use Case Diagram` Scheme

![picture](SDLC/UCD_PostSeminarFeedback.jpg)

- `BPMN` Scheme

![picture](SDLC/BPMN_PostSeminarFeedback.png)