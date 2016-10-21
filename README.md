I'm building a new website and wanted to use:

- F#
- My Mac
- VSCode
- Webpack
- Yarn, because it's new
- Bootstrap
- Templating
- Azure

So this is where I'm up to so far and thought I would share it before I start filling it in.

It uses OWIN and Handlebars, and a simple bootstrap template.

There's Home, Error and Not Found pages, and by going to `/hello/world` there's an example of parameters being passed to a model.

While developing you can run `./dev.sh` to test it locally, and `./build.sh` will create an IIS deployable folder `build`.

`deploy.sh` will deploy it to your Azure website or other git deployable host.