import http from 'k6/http';
import { check, sleep } from "k6";

export let options = {
  stages: [
      // Ramp-up from 1 to TARGET_VUS virtual users (VUs) in 5s
      { duration: "5s", target: 10 },

      // Stay at rest on TARGET_VUS VUs for 10s
      { duration: "10s", target: 10000 },

      // Ramp-down from TARGET_VUS to 0 VUs for 5s
      { duration: "5s", target: 0 }
  ]
};

export default function () {
  const response = http.get("https://host.docker.internal:55003/api/Order", {headers: {Accepts: "*/*"}});
  check(response, { "status is 200": (r) => r.status === 200 });
  sleep(.200);
};
