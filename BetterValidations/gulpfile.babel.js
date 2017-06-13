const gulp = require('gulp'),
    concat = require('gulp-concat'),
    del = require('del'),
    cssnano = require('gulp-cssnano'),
    sass = require('gulp-sass'),
    watch = require('gulp-watch'),
    babel = require('gulp-babel');

const root = './';
const assetsRoot = `${root}Assets/`;
const staticRoot = `${root}wwwroot/`;

const npmDir = `${root}node_modules/`;

const staticDirs = {
    css: `${staticRoot}css/`,
    images: `${staticRoot}images/`,
    scripts: {
        libraries: `${staticRoot}scripts/libraries/`,
        custom: `${staticRoot}scripts/`
    }
};

const npm = {
    scripts: [
		`${npmDir}axios/dist/axios.js`,
		`${npmDir}angular/angular.js`
    ],
    styles: [
        npmDir + 'bootstrap/dist/css/bootstrap.css'
    ]
};

gulp.task('default', ['copy', 'watch']);

gulp.task('clean', ['clean-static']);

gulp.task('clean-static', () => {
    return del(staticRoot + '**/*');
});

gulp.task('clean-dependencies', () => {
    return del([npmDir].map(dir => {
        return dir + '**/*';
    }));
});

gulp.task('copy', [
	'copy-library-styles',
    'copy-custom-sass',
    'copy-library-scripts',
    'copy-custom-scripts'
]);

gulp.task('copy-custom-sass', () => {
    gulp.src(assetsRoot + 'Sass/**/*.scss')
        .pipe(sass().on('error', sass.logError))
        .pipe(concat('style.css'))
        .pipe(cssnano())
        .pipe(gulp.dest(staticDirs.css));
});

gulp.task('copy-library-scripts', () => {
    gulp.src(npm.scripts)
        .pipe(gulp.dest(staticDirs.scripts.libraries));
});

gulp.task('copy-custom-scripts', [
	'copy-custom-vanilla',
	'copy-custom-angular'
]);

gulp.task('copy-custom-vanilla', () => {
	gulp.src([
		`${assetsRoot}Scripts/Vanilla/common.js`,
		`${assetsRoot}Scripts/Vanilla/register.js`
	])
		.pipe(concat('vanilla-example.js'))
		.pipe(babel({
			presets: ['es2015']
		}))
		.pipe(gulp.dest(staticDirs.scripts.custom));
});

gulp.task('copy-custom-angular', () => {
	gulp.src([`${assetsRoot}Scripts/Angular/register.js`])
		.pipe(concat('angular-example.js'))
		.pipe(babel({
			presets: ['es2015']
		}))
		.pipe(gulp.dest(staticDirs.scripts.custom));
});

gulp.task('copy-library-styles', () => {
    gulp.src(npm.styles)
        .pipe(concat('libraries.css'))
        .pipe(cssnano())
        .pipe(gulp.dest(staticDirs.css));
});

gulp.task('watch', ['watch-sass', 'watch-custom-scripts']);

gulp.task('watch-sass', () => {
    gulp.watch(`${assetsRoot}Sass/**/*.scss`, ['copy-custom-sass']);
});

gulp.task('watch-custom-scripts', () => {
    gulp.watch(`${assetsRoot}Scripts/**/*.js`, ['copy-custom-scripts']);
});