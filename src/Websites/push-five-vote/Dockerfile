FROM node:12.4.0 AS build
WORKDIR /app

ENV PATH /app/node_modules/.bin:$PATH

COPY /src/Websites/push-five-vote/package.json /app/package.json
RUN npm install
RUN npm install -g @angular/cli

COPY /src/Websites/push-five-vote/. /app

RUN ng build --prod --output-path=dist

FROM nginx:1.16.0-alpine
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
