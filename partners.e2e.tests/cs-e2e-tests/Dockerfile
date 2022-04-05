FROM mcr.microsoft.com/playwright:bionic

WORKDIR /app

COPY package.json package-lock.json ./

RUN npm config set registry="http://registry.npmjs.org/"
RUN NODE_TLS_REJECT_UNAUTHORIZED=0 npm ci

COPY . .

CMD ["npm", "run", "test:ui:sandobx"]