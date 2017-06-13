((formService, httpService) => {

	const username = document.querySelector('.bv-username'),
		email = document.querySelector('.bv-email'),
		password = document.querySelector('.bv-password'),
		confirm = document.querySelector('.bv-password-confirm'),
		register = document.querySelector('.bv-register-btn');

	formService.setInputUpdateListener(username);
	formService.setInputUpdateListener(email);
	formService.setInputUpdateListener(password);
	formService.setInputUpdateListener(confirm);

	register.addEventListener('click', () => {
		const errorHandler = (error) => {
			if (!!error.UserName) {
				formService.displayHttpError(username, error.UserName);
			}
			if (!!error.Email) {
				formService.displayHttpError(email, error.Email);
			}
			if (!!error.Password) {
				formService.displayHttpError(password, error.Password);
			}
			if (!!error.ConfirmPassword) {
				formService.displayHttpError(confirm, error.ConfirmPassword);
			}
		};

		const registerModel = {
			UserName: username.value,
			Email: email.value,
			Password: password.value,
			ConfirmPassword: confirm.value
		};

		httpService
			.register(registerModel)
			.then(newUserId => {
				console.log(`Successfully created new user with id ${newUserId}.`);
			})
			.catch(errorHandler);
	});

})(QP.FormService, QP.HttpService);