import http from 'k6/http';
import { check, sleep } from "k6";

export let options = {
  stages: [
      // Ramp-up from 1 to TARGET_VUS virtual users (VUs) in 5s
      { duration: "25s", target: 10 },

      // Stay at rest on TARGET_VUS VUs for 10s
      { duration: "5s", target: 5000 },

      // Ramp-down from TARGET_VUS to 0 VUs for 5s
      { duration: "25s", target: 0 }
  ]
};

export default function () {
  const response = http.get("http://20.160.246.194/api/order", {headers: {Accepts: "*/*"}});
  check(response, { "status is 200": (r) => r.status === 200 });
  sleep(.200);
};
