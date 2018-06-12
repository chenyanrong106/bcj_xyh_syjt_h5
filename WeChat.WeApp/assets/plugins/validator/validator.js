﻿/*********************************
 * Themes, rules, and i18n support
 * Locale: Chinese; 中文
 *********************************/

/*! nice Validator 0.6.8
* (c) 2012-2013 Jony Zhang <zj86@live.cn>, MIT Licensed
* http://niceue.com/validator/
*/
!function (e, t) { "use strict"; function i(r, n) { var s = this; return !s instanceof i ? new i(r, n) : (s.$el = e(r), s._init(r, n), t) } function r(e, t) { var i = t ? t === !0 ? this : t : r.prototype; if (P(e)) for (var n in e) i[n] = s(e[n]) } function n(e, t) { var i = t ? t === !0 ? this : t : n.prototype; if (P(e)) for (var r in e) { if (!e[r]) return; i[r] = e[r] } } function s(t) { switch (e.type(t)) { case "function": return t; case "array": return function (e) { return t[0].test(e.value) || t[1] || !1 }; case "regexp": return function (e) { return t.test(e.value) } } } function a(t) { var i = ""; return e.map(t.split(" "), function (e) { i += "," + ("#" === e.charAt(0) ? e : '[name="' + e + '"]') }), i.substring(1) } function l(t) { var i; if (t && t.tagName) { switch (t.tagName) { case "INPUT": case "SELECT": case "TEXTAREA": case "BUTTON": case "FIELDSET": i = t.form || e(t).closest(".n-" + h); break; case "FORM": i = t; break; default: i = e(t).closest(".n-" + h) } return e(i).data(h) || e(i)[h]().data(h) } } function u(t, i) { if (t.form && null === H(t.form, F)) { var r = l(t); r ? (r._parse(t), e(t).trigger(i)) : H(t, x, null) } } function o(i, r) { var n = e.trim(H(i, x + "-" + r)); if (n) return n = Function("return " + n)(), n ? s(n) : t } function d(e, t, i) { var r = t.msg; return P(r) && i && (r = r[i]), W(r) || (r = H(e, "data-msg-" + i) || H(e, "data-msg") || ""), r } function f(e) { var t; return e && (t = S.exec(e)), t ? t[1] : "" } function c(e) { return "INPUT" === e.tagName && "checkbox" === e.type || "radio" === e.type } function g(e) { return Date.parse(e.replace(/\.|\-/g, "/")) } var p, h = "validator", m = "n-ok", v = "n-error", y = "n-tip", b = "n-loading", k = "n-valid", _ = "n-invalid", w = "msg-box", M = "aria-required", O = "aria-invalid", x = "data-rule", $ = "data-target", A = "data-tip", T = "data-inputstatus", F = "novalidate", V = ":verifiable", C = /(\w+)(?:\[(.*)\]$|\((.*)\)$)?/, E = /(?:([^:;\(\[]*):)?(.*)/, R = /[^\x00-\xff]/g, S = /^.*(top|right|bottom|left).*$/, N = /(?:(post|get):)?(.+)/i, j = /<|>/g, q = e.noop, D = e.proxy, I = e.isFunction, L = e.isArray, W = function (e) { return "string" == typeof e }, P = function (e) { return e && "[object Object]" === Object.prototype.toString.call(e) }, U = !window.XMLHttpRequest, H = function (e, i, r) { return r === t ? e.getAttribute(i) : (null === r ? e.removeAttribute(i) : e.setAttribute(i, "" + r), t) }, B = window.console || { log: q, info: q }, X = { debug: 0, timely: 1, theme: "default", stopOnError: !1, ignore: "", msgWrapper: "span", msgMaker: function (e) { var t, i = { error: v, ok: m, tip: y, loading: b}[e.type]; return t = '<span class="msg-wrap ' + i + '" role="alert">', t += (e.arrow || "") + (e.icon || "") + '<span class="n-msg">' + e.msg + "</span>", t += "</span>" }, msgIcon: '<span class="n-icon"></span>', msgArrow: "", msgClass: "", defaultMsg: "{0} is not valid.", loadingMsg: "Validating..." }, J = { "default": { formClass: "n-default", msgClass: "n-right", showOk: ""} }; e.fn[h] = function (t) { var r = this, n = arguments; return r.is(":input") ? r : (!r.is("form") && (r = this.find("form")), !r.length && (r = this), r.each(function () { var r = e(this).data(h); if (r) if (W(t)) { if ("_" === t.charAt(0)) return; r[t].apply(r, Array.prototype.slice.call(n, 1)) } else t && (r._reset(!0), r._init(this, t)); else new i(this, t) }), this) }, e.fn.isValid = function (e, i) { var r, n, s = l(this[0]); return s ? (i === t && (i = !0), s.checkOnly = i, r = this.is(":input") ? this : this.find(V), n = s._multiValidate(r, function (t) { I(e) && e.call(null, t), s.checkOnly = !1 }), I(e) ? this : n) : !0 }, e.expr[":"].verifiable = function (e) { var t = e.nodeName.toLowerCase(); return ("input" === t && "submit" !== e.type && "button" !== e.type && "reset" !== e.type || "select" === t || "textarea" === t) && e.disabled === !1 && null === H(e, F) }, i.prototype = { _init: function (t, i) { var s, l, u, o = this; if (I(i) && (i = { valid: i }), i = i || {}, u = H(t, "data-" + h + "-option"), u = u && "{" === u.charAt(0) ? Function("return " + u)() : {}, l = J[i.theme || u.theme || X.theme], s = o.options = e.extend({}, X, l, u, this.options, i), o.rules = new r(s.rules, !0), o.messages = new n(s.messages, !0), o.elements = o.elements || {}, o.deferred = {}, o.errors = {}, o.fields = {}, o._initFields(s.fields), L(s.groups) && e.map(s.groups, function (t) { if (!W(t.fields) || !I(t.callback)) return null; var i = o.$el.find(a(t.fields)), r = function () { return t.callback.call(o, i) }; e.extend(r, t), e.map(t.fields.split(" "), function (e) { o.fields[e] = o.fields[e] || {}, o.fields[e].group = r }) }), o.msgOpt = { type: "error", pos: f(s.msgClass), wrapper: s.msgWrapper, cls: s.msgClass, style: s.msgStyle, icon: s.msgIcon, arrow: s.msgArrow, show: s.msgShow, hide: s.msgHide }, o.isAjaxSubmit = !1, s.valid || null === H(t, "action")) o.isAjaxSubmit = !0; else { var d = e[e._data ? "_data" : "data"](t, "events"); d && d.valid && e.map(d.valid, function (e) { return -1 !== e.namespace.indexOf("form") ? 1 : null }).length && (o.isAjaxSubmit = !0) } o.$el.data(h) || (o.$el.data(h, o).addClass("n-" + h + " " + s.formClass).on("submit." + h + " validate." + h, D(o, "_submit")).on("reset." + h, D(o, "_reset")).on("showtip." + h, D(o, "_showTip")).on("validated.field." + h, V, D(o, "_validatedField")).on("validated.rule." + h, V, D(o, "_validatedRule")).on("focusin." + h + " click." + h + " showtip." + h, V, D(o, "_focus")).on("focusout." + h + " validate." + h, V, D(o, "_blur")).on("click." + h, "input:radio,input:checkbox", D(o, "_click")), s.timely >= 2 && o.$el.on("keyup." + h + " paste." + h, V, D(o, "_blur")).on("change." + h, "select", D(o, "_click")), o.NOVALIDATE = H(t, F), H(t, F, F)) }, _initFields: function (t) { var i = this; P(t) && e.each(t, function (e, t) { var r = i.elements[e]; !t && r && i._resetElement(r, !0), i.fields[e] = W(t) ? { rule: t} : t }), i.$el.find(V).each(function () { i._parse(this) }) }, _multiValidate: function (i, r) { var n = this, s = n.options; return n.isValid = !0, n.deferred = {}, s.ignore && (i = i.not(s.ignore)), i.each(function (e, i) { var r = n.getField(i); if (r) return n._validate(i, r), !n.isValid && s.stopOnError ? !1 : t }), e.when.apply(null, e.map(n.deferred, function (e) { return e })).done(function () { r.call(n, n.isValid) }), e.isEmptyObject(n.deferred) ? n.isValid : t }, _submit: function (i, r) { var n = this, s = n.options, a = i.target; if (p) return p = !1, t; if ("only" !== r && ("validate" !== i.type || n.$el[0] === a)) return i.preventDefault(), n.submiting ? (I(n.submiting) && n.submiting.call(n), t) : (I(s.beforeSubmit) && s.beforeSubmit.call(n, a) === !1 || (n._reset(), n.submiting = !0, s.debug && B.log("\n" + i.type + " form"), n._multiValidate(n.$el.find(V), function (t) { var i, r = "focus.field", l = t || 2 === s.debug ? "valid" : "invalid"; if (!t) { var u = n.$el.find(":input[" + O + '="true"]:first'); u.trigger(r), U && u.trigger(r), i = e.map(n.errors, function (e) { return e }) } n.submiting = !1, I(s[l]) && s[l].call(n, a, i), n.$el.trigger(l + ".form", [a, i]), t && !n.isAjaxSubmit && (p = !0, a.submit()) })), t) }, _reset: function (e) { var t = this; t.errors = {}, e && t.$el.find(V).each(function (e, i) { t._resetElement(i) }) }, _resetElement: function (t, i) { e(t).removeClass(k + " " + _), this.hideMsg(t), i && H(t, M, null) }, _focus: function (e) { var t, i = e.target; if ("showtip" !== e.type) { if (e.isTrigger || this.submiting) return; if ("" !== i.value && ("false" === H(i, O) || "tip" === H(i, T))) return } t = H(i, A), t && this.showMsg(i, { msg: t, type: "tip" }) }, _blur: function (t, i) { var r, n, s = this, a = s.options, l = t.target, u = t.type, o = 150; if (!i && "paste" !== u) { if ("validate" === u) n = !0, o = 0; else { if (H(l, "notimely")) return; if (a.timely >= 2 && "keyup" !== u) return } if (a.ignore && e(l).is(a.ignore)) return; if ("keyup" === u) { var d = t.keyCode, f = { 8: 1, 9: 1, 16: 1, 32: 1, 46: 1 }; if (9 === d && !l.value) return; if (48 > d && !f[d]) return; o = a.timely >= 100 ? a.timely : 500 } } r = s.getField(l), r && (o ? (r.timeout && clearTimeout(r.timeout), r.timeout = setTimeout(function () { s._validate(l, r, n) }, o)) : s._validate(l, r, n)) }, _click: function (e) { this._blur(e, !0) }, _showTip: function (e) { var t = this; t.$el[0] === e.target && t.$el.find(V + "[" + A + "]").each(function () { t.showMsg(this, { msg: H(this, A), type: "tip" }) }) }, _parse: function (e) { var t, i = this, r = e.name, n = H(e, x); n && H(e, x, null), (e.id && "#" + e.id in i.fields || !e.name) && (r = "#" + e.id), r && (t = i.fields[r] || {}, t.key = r, t.old = {}, null !== i.fields[r] && (t.rule = t.rule || n || ""), t.rule && (t.rule.match(/match|checked/) && (t.must = !0), -1 !== t.rule.indexOf("required") && (t.required = !0, H(e, M, !0)), ("timely" in t && !t.timely || !i.options.timely) && H(e, "notimely", !0), W(t.target) && H(e, $, t.target), W(t.tip) && H(e, A, t.tip), i.fields[r] = i._parseRule(t))) }, _parseRule: function (i) { var r, n = E.exec(i.rule); if (n) return i.display = n[1], i.rules = [], r = (n[2] || "").split(";"), e.map(r, function (r) { var n = C.exec(r); return n ? (n[3] && (n[2] = n[3]), i.rules.push({ method: n[1], params: n[2] ? e.trim(n[2]).split(", ") : t }), t) : null }), i.vid = 0, i.rid = i.rules[0].method, i }, _validatedField: function (t, i, r) { var n = this, s = n.options, a = t.target, l = r.isValid = i.isValid = !!r.isValid, u = l ? "valid" : "invalid"; r.key = i.key, r.rule = i.rid, l ? r.type = "ok" : (n.submiting && (n.errors[i.key] = r.msg), n.isValid = !1), i.old.value = a.value, i.old.id = a.id, n.elements[i.key] = a, n.checkOnly || (I(i[u]) && i[u].call(n, a, r), e(a).attr(O, l ? null : !0).removeClass(l ? _ : k).addClass(r.skip ? "" : l ? k : _).trigger(u + ".field", [r, n]), (i.msgMaker || s.msgMaker) && (!r.showOk && r.msg || r.showOk && s.showOk !== !1 ? n.showMsg(a, r, i) : n.hideMsg(a, r, i))) }, _validatedRule: function (i, r, n, s) { var a, l = this, u = l.options, o = i.target, f = "", c = !1, g = !1; if (s = s || {}, r = r || l.getField(o), a = r.rid, null === n) return e(o).trigger("validated.field", [r, { isValid: !0, skip: !0}]), t; if (n === !0 || n === t ? c = !0 : (f = d(o, r, a), f || (W(n) ? (f = n, n = { error: f }) : P(n) && (n.error ? f = n.error : (c = !0, n.ok && W(n.ok) && (g = !0), f = n.ok))), s.msg = (c ? f : f || l.messages[a] || X.defaultMsg).replace("{0}", r.display || "")), u.debug && B.log("   " + r.vid + ": " + a + " => " + (s.msg || !0)), c) { if (s.isValid = !0, !g) { var p = r.ok || H(o, "data-ok"); p ? (g = !0, s.msg = p) : W(u.showOk) && (g = !0, s.msg = u.showOk) } s.showOk = g, e(o).trigger("valid.rule", [a, s.msg]) } else e(o).trigger("invalid.rule", [a, s.msg]); c && r.vid < r.rules.length - 1 ? (r.vid++, l._checkRule(o, r)) : (r.vid = 0, e(o).trigger("validated.field", [r, s])) }, _checkRule: function (i, r) { var n, s, a = this, l = r.key, u = r.rules[r.vid], d = u.method, f = u.params; if (!a.submiting || !a.deferred[l]) if (s = r.old, r.rid = d, n = !r.must && s.ret !== t && s.rule === u && s.id === i.id && i.value && s.value === i.value ? s.ret : (o(i, d) || a.rules[d] || function () { return !0 }).call(a, i, f, r), P(n) && I(n.then)) { var c = function (e) { return W(e) || P(e) && ("error" in e || "ok" in e) ? e : t }; a.deferred[l] = n, !a.checkOnly && a.showMsg(i, { type: "loading", msg: a.options.loadingMsg }, r), n.then(function (n, l, o) { var d, f = o.responseText, g = r.dataFilter || a.options.dataFilter; "json" === this.dataType ? f = n : "{" === f.charAt(0) && (f = e.parseJSON(f) || {}), I(g) ? f = g(f) : "" === f ? f = !0 : (d = c(f), d === t && (d = c(f.data)), f = d || !0), s.rule = u, s.ret = f, e(i).trigger("validated.rule", [r, f]) }, function (t, n) { e(i).trigger("validated.rule", [r, n]) }), r.isValid = t } else e(i).trigger("validated.rule", [r, n]) }, _validate: function (i, r) { if (!i.disabled && null === H(i, F)) { r.rules || this._parse(i); var n, s = this, a = s.options, l = e(i), u = {}, o = r.group, d = r.isValid = !0; if (a.debug && B.info(r.key), o && (n = o.call(s), n === !0 || n === t ? n = t : (W(n) && (n = { error: n }), r.vid = 0, r.rid = "group", d = !1, s.hideMsg(i, {}, r), e.extend(u, o))), d && !r.required && !r.must && !i.value) { if ("tip" === H(i, T)) return; if (!c(i)) return l.trigger("validated.field", [r, { isValid: !0}]), t } n !== t ? l.trigger("validated.rule", [r, n, u]) : r.rule && s._checkRule(i, r) } }, _getMsgOpt: function (t) { return e.extend({}, this.msgOpt, W(t) ? { msg: t} : t) }, getField: function (e) { var t, i = this; return t = e.id && "#" + e.id in i.fields || !e.name ? "#" + e.id : e.name, H(e, x) && i._parse(e), i.fields[t] }, test: function (i, r) { var n, s, a, l = this, u = C.exec(r); return u ? (u[3] && (u[2] = u[3]), s = u[1], a = u[2] ? e.trim(u[2]).split(", ") : t, s in l.rules && (n = l.rules[s].call(l, i, a)), n === !0 || n === t || null === n || !1) : !0 }, getRangeMsg: function (e, t, i, r) { if (t) { var n = this, s = n.messages[i] || "", a = t[0].split("~"), l = a[0], u = a[1], o = "rg", d = [""], f = +e === +e; if (2 === a.length) { if (l && u) { if (f && e >= +l && +u >= e) return !0; d = d.concat(a) } else if (l && !u) { if (f && e >= +l) return !0; d.push(l), o = "gt" } else if (!l && u) { if (f && +u >= e) return !0; d.push(u), o = "lt" } } else { if (e === +l) return !0; d.push(l), o = "eq" } return s && (r && s[o + r] && (o += r), d[0] = s[o]), n.renderMsg.apply(null, d) } }, renderMsg: function () { var e = arguments, t = e[0], i = e.length; if (t) { for (; --i; ) t = t.replace("{" + i + "}", e[i]); return t } }, _getMsgDOM: function (t, i) { var r, n, s, a = e(t); if (a.is(":input") ? (s = i.target || H(t, $), s && (s = this.$el.find(s), s.length && (s.is(":input") ? t = s.get(0) : r = s)), r || (n = !c(t) && t.id ? t.id : t.name, r = this.$el.find(i.wrapper + "." + w + '[for="' + n + '"]'))) : r = a, !r.length) if (a = this.$el.find(s || t), r = e("<" + i.wrapper + ">").attr({ "class": w + (i.cls ? " " + i.cls : ""), style: i.style || "", "for": n }), c(t)) { var l = a.parent(); r.appendTo(l.is("label") ? l.parent() : l) } else r[i.pos && "right" !== i.pos ? "insertBefore" : "insertAfter"](a); return r }, showMsg: function (t, i, r) { if (i = this._getMsgOpt(i), i.msg || i.showOk) { t = e(t).get(0), e(t).is(V) && (H(t, T, i.type), r = r || this.getField(t), r && (r.msgStyle && (i.style = r.msgStyle), r.msgClass && (i.cls = r.msgClass), r.msgWrapper && (i.wrapper = r.msgWrapper))); var n = this._getMsgDOM(t, i), s = n[0].className; !S.test(s) && n.addClass(i.cls), U && "bottom" === i.pos && (n[0].style.marginTop = e(t).outerHeight() + "px"), n.html(((r || {}).msgMaker || this.options.msgMaker).call(this, i)), n[0].style.display = "", I(i.show) && i.show.call(this, n, i.type) } }, hideMsg: function (t, i, r) { t = e(t).get(0), i = this._getMsgOpt(i), e(t).is(V) && (H(t, T, null), H(t, O, null), r = r || this.getField(t), r && r.msgWrapper && (i.wrapper = r.msgWrapper)); var n = this._getMsgDOM(t, i); n.length && (I(i.hide) ? i.hide.call(this, n, i.type) : n[0].style.display = "none") }, mapMsg: function (t) { var i = this; e.each(t, function (e, t) { var r = i.elements[e] || i.$el.find(':input[name="' + e + '"]')[0]; i.showMsg(r, t) }) }, setMsg: function (e) { new n(e, this.messages) }, setRule: function (t) { new r(t, this.rules), e.map(this.fields, function (e) { e.old = {} }) }, setField: function (e, t) { var i = {}; W(e) ? i[e] = t : P(e) && (i = e), this._initFields(i) }, holdSubmit: function (e) { e === t && (e = !0), this.submiting = e }, destroy: function () { this._reset(!0), this.$el.off("." + h).removeData(h), H(this.$el[0], F, this.NOVALIDATE) } }, e(document).on("focusin", ":input[" + x + "]", function () { u(this, "focusin") }).on("click", "input,button", function () { if (this.form) if ("submit" === this.type) null !== H(this, F) && (p = !0); else if (this.name && c(this)) { var e = this.form.elements[this.name]; e.length && (e = e[0]), H(e, x) && u(e, "validate") } }).on("submit", "form", function (t) { if (null === H(this, F)) { var i, r = e(this); r.data(h) || (i = r[h]().data(h), e.isEmptyObject(i.fields) ? (H(this, F, F), r.off("." + h).removeData(h)) : "submit" === t.type && i._submit(t)) } }), new r({ required: function (t, i) { var r = e.trim(t.value), n = !0; if (i) if (1 === i.length) { if (!r && !this.test(t, i[0])) return H(t, M, null), null; H(t, M, !0) } else "not" === i[0] && e.map(i.slice(1), function (t) { r === e.trim(t) && (n = !1) }); return n && !!r }, integer: function (e, t) { var i, r = "0|", n = "[1-9]\\d*", s = t ? t[0] : "*"; switch (s) { case "+": i = n; break; case "-": i = "-" + n; break; case "+0": i = r + n; break; case "-0": i = r + "-" + n; break; default: i = r + "-?" + n } return i = "^(?:" + i + ")$", RegExp(i).test(e.value) || this.messages.integer[s] }, match: function (t, i, r) { if (i) { var n, s, a, l, u, o, d, f = "eq"; if (1 === i.length ? a = i[0] : (f = i[0], a = i[1]), u = "#" === a.charAt(0) ? a : ':input[name="' + a + '"]', o = this.$el.find(u)[0]) { if (d = this.getField(o), n = t.value, s = o.value, r.init_match || (this.$el.on("valid.field." + h, u, function () { e(t).trigger("validate") }), r.init_match = d.init_match = 1), !r.required && "" === n && "" === s) return null; if (i[2] && ("date" === i[2] ? (n = g(n), s = g(s)) : "time" === i[2] && (n = +n.replace(":", ""), s = +s.replace(":", ""))), "eq" !== f && !isNaN(+n) && isNaN(+s)) return !0; switch (l = this.messages.match[f].replace("{1}", d.display || a), f) { case "lt": return +s > +n || l; case "lte": return +s >= +n || l; case "gte": return +n >= +s || l; case "gt": return +n > +s || l; case "neq": return n !== s || l; default: return n === s || l } } } }, range: function (e, t) { return this.getRangeMsg(+e.value, t, "range") }, checked: function (t, i, r) { if (c(t)) { var n, s; return s = this.$el.find('input[name="' + t.name + '"]').filter(function () { return !n && c(this) && (n = this), !this.disabled && this.checked && e(this).is(":visible") }).length, i ? this.getRangeMsg(s, i, "checked") : !!s || d(n, r, "checked") || this.messages.required } }, length: function (e, t) { var i = e.value, r = (t[1] ? i.replace(R, "xx") : i).length; return t && "~" === t[0].charAt(0) && (t[0] = "0" + t[0]), this.getRangeMsg(r, t, "length", t[1] ? "_2" : "") }, remote: function (t, i) { if (i) { var r, n = this, s = N.exec(i[0]), a = s[2], l = (s[1] || "POST").toUpperCase(), u = {}; return u[t.name] = t.value, i[1] && e.map(i.slice(1), function (t) { u[e.trim(t)] = n.$el.find(':input[name="' + t + '"]').val() }), u = e.param(u), "POST" === l && (r = a.indexOf("?"), -1 !== r && (u += "&" + a.substring(r + 1, a.length), a = a.substring(0, r))), e.ajax({ url: a, type: l, data: u, async: !0, cache: !1 }) } }, filter: function (e, t) { e.value = e.value.replace(t ? RegExp("[" + t[0] + "]", "g") : j, "") } }), i.config = function (t) { e.each(t, function (e, t) { "rules" === e ? new r(t) : "messages" === e ? new n(t) : X[e] = t }) }, i.setTheme = function (t, i) { P(t) ? e.each(t, function (e, t) { J[e] = t }) : W(t) && P(i) && (J[t] = i) }, e[h] = i } (jQuery);


(function ($) {
    /* Global configuration
    */
    $.validator.config({
        //stopOnError: false,
        theme: 'yellow_right_effect',
        defaultMsg: "{0}格式不正确",
        loadingMsg: "正在验证...",

        // Custom rules
        rules: {
            digits: [/^\d+$/, "请输入数字"]
            , letters: [/^[a-z]+$/i, "{0}只能输入字母"]
            , tel: [/^(?:(?:0\d{2,3}[- ]?[1-9]\d{6,7})|(?:[48]00[- ]?[1-9]\d{6}))$/, "电话格式不正确"]
            , mobile: [/^1[3-9]\d{9}$/, "手机号格式不正确"]
            , email: [/^(?:[a-z0-9]+[_\-+.]?)*[a-z0-9]+@(?:([a-z0-9]+-?)*[a-z0-9]+\.)+([a-z]{2,})+$/i, "邮箱格式不正确"]
            , qq: [/^[1-9]\d{4,}$/, "QQ号格式不正确"]
            , date: [/^\d{4}-\d{1,2}-\d{1,2}$/, "请输入正确的日期,例:yyyy-mm-dd"]
            , time: [/^([01]\d|2[0-3])(:[0-5]\d){1,2}$/, "请输入正确的时间,例:14:30或14:30:00"]
            , idcard: [/^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[A-Z])$/, "请输入正确的身份证号码"]
            , url: [/^(https?|ftp):\/\/[^\s]+$/i, "网址格式不正确"]
            , postcode: [/^[1-9]\d{5}$/, "邮政编码格式不正确"]
            , chinese: [/^[\u0391-\uFFE5]+$/, "请输入中文"]
            , number:[/^([\d\.]+)$/,"请输入有效的数字"]
            , username: [/^\w{3,12}$/, "请输入3-12位数字、字母、下划线"]
            , password: [/^[0-9a-zA-Z]{6,16}$/, "密码由6-16位数字、字母组成"]
            , accept: function (element, params) {
                if (!params) return true;
                var ext = params[0];
                return (ext === '*') ||
                       (new RegExp(".(?:" + (ext || "png|jpg|jpeg|gif") + ")$", "i")).test(element.value) ||
                       this.renderMsg("只接受{1}后缀", ext.replace('|', ','));
            }

        }
    });
    /* Default error messages
    */
    $.validator.config({
        messages: {
            required: "{0}必填项",
            remote: "{0}已被使用",
            integer: {
                '*': "请输入整数",
                '+': "请输入正整数",
                '+0': "请输入正整数或0",
                '-': "请输入负整数",
                '-0': "请输入负整数或0"
            },
            match: {
                eq: "{0}与{1}不一致",
                neq: "{0}与{1}不能相同",
                lt: "{0}必须小于{1}",
                gt: "{0}必须大于{1}",
                lte: "{0}必须小于或等于{1}",
                gte: "{0}必须大于或等于{1}"
            },
            range: {
                rg: "请输入{1}到{2}的数",
                gt: "请输入大于或等于{1}的数",
                lt: "请输入小于或等于{1}的数"
            },
            checked: {
                eq: "请选择{1}项",
                rg: "请选择{1}到{2}项",
                gt: "请至少选择{1}项",
                lt: "请最多选择{1}项"
            },
            length: {
                eq: "请输入{1}个字符",
                rg: "字符太长",
                gt: "请输入大于{1}个字符",
                lt: "字符太长",
                eq_2: "",
                rg_2: "",
                gt_2: "",
                lt_2: ""
            }
        }
    });

    /* Themes
    */
    var TPL_ARROW = '<span class="n-arrow"><b>◆</b><i>◆</i></span>';
    $.validator.setTheme({
        'simple_right': {
            formClass: 'n-simple',
            msgClass: 'n-right'
        },
        'simple_bottom': {
            formClass: 'n-simple',
            msgClass: 'n-bottom'
        },
        'yellow_top': {
            formClass: 'n-yellow',
            msgClass: 'n-top',
            msgArrow: TPL_ARROW
        },
        'yellow_right': {
            formClass: 'n-yellow',
            msgClass: 'n-right',
            msgArrow: TPL_ARROW
        },
        'yellow_right_effect': {
            formClass: 'n-yellow',
            msgClass: 'n-bottom',
            msgArrow: TPL_ARROW,
            msgShow: function ($msgbox, type) {
                var $el = $msgbox.children();
                if ($el.is(':animated')) return;
                if (type === 'error') {
                    $el.css({
                        top: '20px',
                        opacity: 0
                    }).delay(100).show().stop().animate({
                        top: '-4px',
                        opacity: 1
                    }, 150).animate({
                        top: '3px'
                    }, 80).animate({
                        top: 0
                    }, 80);
                } else {
                    $el.css({
                        left: 0,
                        opacity: 1
                    }).fadeIn(200);
                }
            },
            msgHide: function ($msgbox, type) {
                var $el = $msgbox.children();
                $el.stop().delay(100).show().animate({
                    left: '20px',
                    opacity: 0
                }, 300, function () {
                    $msgbox.hide();
                });
            }
        }
    });
})(jQuery);