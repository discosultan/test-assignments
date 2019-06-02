# TODO: Don't run as root for added runtime security.

FROM node:12.3-alpine
LABEL maintainer="Jaanus Varus <jaanusvarus@gmail.com>"
LABEL name="embark-user"

WORKDIR /home/app
COPY . .

# We need to rebuild bcrypt from source to target musl instead of glibc for Alpine.
RUN apk --no-cache add --virtual .build-deps build-base python && \
    npm install && \
    npm rebuild bcrypt --build-from-source && \
    apk del .build-deps

EXPOSE 8000
ENTRYPOINT ["/usr/local/bin/npm", "start"]
