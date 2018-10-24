module.exports = function(grunt) {

    "use strict";
    
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        watch: {
            errors : {
                files: [
                    '../Plugins/Admin/Modules/Errors/Js/**/*.js'
                ],
                tasks: ['concat:errors', 'uglify:errors']
            },
            errorsHTML : {
                files: [
                    '../Plugins/Admin/Modules/Errors/Js/**/*.html'
                ],
                tasks: ['concat:errorsHTML']
            },
            options: {
                spawn: false
            }
        },

        concat: {
            errors: {
                src: [
                    '../Plugins/Admin/Modules/Errors/Js/Bootstrap/*.js',
                    '../Plugins/Admin/Modules/Errors/Js/DataServices/*.js',
                    '../Plugins/Admin/Modules/Errors/Js/UIComponents/*.js',
                    '../Plugins/Admin/Modules/Errors/Js/Routes/*.js',
                ],
                dest: '../Plugins/Admin/Modules/Errors/Content/js/errors.js'
            },
            errorsHTML: getHtmlTemplateConfig('Errors'),
        },

        uglify: {
            options: {
                banner: '/*! <%= pkg.name %> <%= grunt.template.today("yyyy-mm-dd") %> */\n'
            },
            errors: {
                src: '../Plugins/Admin/Modules/Errors/Content/js/errors.js',
                dest: '../Plugins/Admin/Modules/Errors/Content/js/errors_min.js'
            },
        }
    });
    
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-concat');

    grunt.registerTask('default', ['bundleJS']);
    grunt.registerTask('bundleJS', [
       
        'concat:errors',
        'concat:errorsHTML',
        'uglify',
    ]);
	
	
	function getHtmlTemplateConfig(moduleName) {
		return {
			options: {
				banner: "angular.module('cms." + camelize(moduleName) + "').run(['$templateCache',function(t){",
				footer: "}]);",
				process: function(src, filepath) {
					var removeSpaces = src.replace(/[\t\n\r]/gm, "");
					var escapeQuotes = removeSpaces.replace(/'/g, "\\'");
					var splitPath = filepath.split('..');
					var formattedSrc = "t.put('" + splitPath[1] + "','" + escapeQuotes + "');";
					
					return formattedSrc;
				}
			},
			src: [
				'../Plugins/Admin/Modules/' + moduleName + '/Js/Routes/*.html',
				'../Plugins/Admin/Modules/' + moduleName + '/Js/Routes/**/*.html',
				'../Plugins/Admin/Modules/' + moduleName + '/Js/UIComponents/*.html',
				'../Plugins/Admin/Modules/' + moduleName + '/Js/UIComponents/**/*.html'
			],
			dest: '../Plugins/Admin/Modules/' + moduleName + '/Content/js/' + moduleName.toLowerCase() + '_templates.js'
		}
	}
	
	function camelize(string) {
		return string.charAt(0).toLowerCase() + string.slice(1);
	}
};