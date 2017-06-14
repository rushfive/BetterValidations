const BV = {};

BV.FormService = (() => {
	return {
		setInputUpdateListener: (inputElement, callback) => {
			inputElement.addEventListener('input', event => {
				inputElement.style.borderColor = '#cccccc';
				const errorTextNode = inputElement.nextElementSibling;
				errorTextNode.innerHTML = null;
				errorTextNode.style.display = 'none';

				if (!!callback) {
					callback();
				}
			});
		},
		displayHttpError: (inputElement, errorText) => {
			inputElement.style.borderColor = 'red';
			const errorTextNode = inputElement.nextElementSibling;
			errorTextNode.innerHTML = errorText;
			errorTextNode.style.display = 'block';
		}
	}
})();

BV.HttpService = ((x) => {
	// add axios response interceptor
	x.interceptors.response.use(
		response => response,
		error => {
			if (error.response) {
				const responseErrors = error.response.data;
				const errors = {};
				for (let key in responseErrors) {
					if (responseErrors.hasOwnProperty(key)) {
						errors[key] = responseErrors[key][0];
					}
				}
				return Promise.reject(errors);
			} else {
				return Promise.reject(error);
			}

		});

	return {
		register: (registerModel) => {
			return x.post('/api/User/Register', registerModel);
		}
	}
})(axios);