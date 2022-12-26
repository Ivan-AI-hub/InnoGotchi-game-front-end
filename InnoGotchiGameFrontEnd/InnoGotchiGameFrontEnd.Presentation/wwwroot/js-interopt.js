function message(e) { alert(e); }
function set(key, value) { localStorage.setItem(key, value); }
function get(key) { return localStorage.getItem(key); }
function remove(key) { return localStorage.removeItem(key); }
function getInnerHTML(element) { return new XMLSerializer().serializeToString(element); }