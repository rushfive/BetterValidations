const app = angular.module('angularExampleApp', []);

class AngularInputFormDirective {
	constructor() {
		this.templateUrl = 'angular-input-form-template.html';
		this.restrict = 'A';
		this.scope = {
			value: '=angularInputForm',
			label: '@label',
			error: '=error',
			inputType: '@inputType'
		};
	}
}

// We create a wrapper around angular's $http so we can modify the
// response/error before returning to the caller. This is analagous to
// our usage of axios's http interceptors in the vanilla js example.
class HttpService {
	constructor($http, $q) {
		this.$http = $http;
		this.$q = $q;
	}

	post(url, model) {
		return this.$http.post(url, model)
			.then(response => {
				return response.data;
			})
			.catch(error => {
				for (let key in error.data) {
					if (error.data.hasOwnProperty(key)) {
						error.data[key] = error.data[key][0];
					}
				}
				return this.$q.reject(error.data);
			});
	}
}

class UserService {
	constructor(httpService) {
		this.httpService = httpService;
	}

	register(user) {
		// user must be pascal cased to match backend models
		const model = {
			UserName: user.userName,
			Email: user.email,
			Password: user.password,
			ConfirmPassword: user.confirmPassword
		};

		return this.httpService.post('/api/User/Register', model);
	}
}

class Controller {
	constructor(userService) {
		this.userService = userService;

		this.model = {
			userName: null,
			email: null,
			password: null,
			confirmPassword: null
		};

		this.errors = {};
	}

	register() {
		this.userService.register(this.model)
			.then(newUserId => {
				console.log(`Successfully registered new user with id ${newUserId}.`);
			})
			.catch(errors => {
				this.errors = errors;
			});
	}
}

app
	.directive('angularInputForm', () => new AngularInputFormDirective())
	.service('httpService', [
		'$http', '$q',
		($http, $q) => new HttpService($http, $q)
	])
	.service('userService', [
		'httpService',
		(httpService) => new UserService(httpService)
	])
	.controller('controller', [
		'userService',
		(userService) => new Controller(userService)
	]);