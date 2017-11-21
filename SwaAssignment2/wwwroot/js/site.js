var employeeListApp = angular.module('employeeListApp', []);

class Person {
    constructor(id, name, email, phone, address, picture, nationality, salary) {
        this.id = id;
        this.name = name;
        this.email = email;
        this.phone = phone;
        this.address = address;
        this.picture = picture;
        this.nationality = nationality;
        this.salary = salary;
    }

    toString() {
        return `${this.name.toString()} ${this.email} ${this.phone} ${this.address.toString()} ${this.nationality} ${this.salary}`;
    }
}

class Name {
    constructor(firstName, lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
    }

    toString() {
        return `${this.firstName} ${this.lastName}`;
    }
}

class Location {
    constructor(city, postcode, state, street) {
        this.city = city;
        this.postcode = postcode;
        this.state = state;
        this.street = street;
    }

    toString() {
        return `${this.postcode}, ${this.city}, ${this.state}, ${this.street}`;
    }
}

employeeListApp.filter('filterEmployees', function () {
    return function (employees, letter) {
        if (letter === undefined) {
            return employees;
        }
        return employees.filter(function (employee) {
            return employee.toString().toLowerCase().includes(letter);
        });
    };
});

employeeListApp.controller('EmployeeListController', function ($scope, $http) {
    $scope.randomUserToPerson = function (user) {
        var salary = user.salary;
        var name = new Name(user.name.first, user.name.last);
        var location = new Location(user.location.city, user.location.postcode, user.location.state, user.location.street);
        var person = new Person(user.login.username, name, user.email, user.phone, location, user.picture.large, user.nat, salary);
        return person;
    };
    $scope.employees = [];
    $scope.getFlagUrl = function (nationality) {
        return `https://lipis.github.io/flag-icon-css/flags/4x3/${nationality.toLowerCase()}.svg`;
    };
    $scope.totalSalary = function () {
        var salaries = [];
        for (var i = 0; i < $scope.employees.length; i++)
            salaries.push($scope.employees[i].salary);
        return salaries.length ? salaries.reduce(function (sum, value) { return sum + value; }) : 0;
    };
    $scope.loadData = function () {
        $http.get("/api/Users")
            .then(function (result) {
                const data = result.data
                    .map(obj => Object.assign(new Person, obj));
                // figure out a better way to do this
                //.map(obj => Object.assign(new Location, obj.nationality))
                //.map(obj => Object.assign(new Name, obj.name));
                $scope.employees = data;
            });
    };
    $scope.fieldValidation = function (key) {
        return $scope.addUserForm[key].$invalid;
    };
    $scope.addEmployee = function (user) {
        const newUser = $scope.randomUserToPerson(user);
        $scope.user = {};
        $http
            .post("/api/Users/", newUser)
            .then(function () {
                $scope.employees.push(newUser);
            }, function () {
                console.log("Erro posting new user");
            });
    };
    $scope.removeEmployee = function (id) {
        if (confirm("Are you sure you want to remove the employee?\nThis action is irreversible and you will have to add him back manually!")) {
            for (let i = 0; i < $scope.employees.length; i++) {
                if ($scope.employees[i].id.toLowerCase() === id.toLowerCase()) {
                    const indexToRemove = i;
                    $http
                        .delete(`/api/Users/${id}`)
                        .then(function () {
                            $scope.employees.splice(indexToRemove, 1);
                        }, function () {
                            console.log("Erro posting new user");
                        });
                }
            }
        }
    };
    $scope.loadMoreUsersOnServer = function() {
        $http.get("/api/UserDatabase")
            .then(function () {
                $scope.loadData();
            });
    }
});
