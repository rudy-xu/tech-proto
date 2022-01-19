FROM 10.86.9.106/hyva/nginx:latest
MAINTAINER ruilong.xu@sioux.asia

RUN rm /etc/nginx/conf.d/default.conf
ADD default.conf /etc/nginx/conf.d/
COPY dist/  /usr/share/nginx/html/
