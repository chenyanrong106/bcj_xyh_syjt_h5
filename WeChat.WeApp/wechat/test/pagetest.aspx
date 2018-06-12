﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagetest.aspx.cs" Inherits="SPACRM.WebApp.wechat.test.pagetest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .cpm-dialog-mod, .cpm-mask {
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            bottom: 0;
            right: 0;
        }

        *, a {
            -webkit-tap-highlight-color: transparent;
        }

        .a_more:after, .rec_list .img_wrap:after {
            content: " ";
        }

        .cpm-dialog, .match_info, .rankboard_head, .tab .tab_hd, .tab .tab_hd a, .tap_more, footer {
            text-align: center;
        }

        .fl {
            float: left;
        }

        .fr {
            float: right;
        }

        body {
            margin: 0;
            padding: 0;
            -webkit-text-size-adjust: 100%!important;
        }

        .none {
            display: none;
        }

        input[type=number]::-webkit-inner-spin-button, input[type=number]::-webkit-outer-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        .fb {
            font-weight: 700;
        }

        .cpm-mask {
            position: absolute;
            z-index: 96;
            background-color: rgba(0,0,0,.7);
        }

        .cpm-dialog-mod {
            position: fixed;
            z-index: 100;
            display: -webkit-box;
            display: -ms-flexbox;
            -webkit-box-orient: vertical;
            -webkit-box-pack: center;
            -webkit-box-align: center;
            -ms-flex-direction: column;
            -ms-flex-pack: center;
            -ms-flex-align: center;
        }

        .cpm-dialog {
            position: relative;
            z-index: 100;
            width: auto;
            min-width: 170px;
            margin: 2px 20px;
            background: rgba(0,0,0,.7);
            border-radius: 5px;
            color: #fff;
        }

            .cpm-dialog .i-delete:before, .cpm-dialog .icon-load {
                background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAIAAAACgBAMAAAA2vHnOAAAAElBMVEX////////d3d27u7uZmZlmZmZIWkRRAAAAAXRSTlMAQObYZgAAAjlJREFUeF7t10Fu4zAMhWFNou4nNwh8ggC6gBY8QDe9/1VmNz8SRiafgghBYW4a1OKHR7mOq/Lxdbm8Bvy5XP6+GIAI64ADOG3XMVCth8C2bWPAzOIAGxEegWpmfQWwjQDLAtfnQM0AJQJKEWYAYIJ3A8wAkJ8AYHsGWBI47QE9C1w9ULNA2QNKEWYAYIIFADMAuAmEGwlgAnAaAV0BrgDSTWQTeLVltqC1wQzDCe65c2s3D1AeqPdea/fCcwDOTdQQAoB+DyCUE0/DY2T6PdAQCUAENHPAGSEo+klEhJxgBAgEoZ8hbhFQGSAQpH4v6P1sg7oBFBGiACM/C4wX5IAyrnMG6CUQ9q7TPxSi29jLh9ZRRxllNrG8AsSPil+OiBkHoH4B8PImvnwbP7SOOqpan7zMozL7eudgI1+nLANY0B8CY8GygI378cU16AQQFrl+VfC0LujvxqBfEsg1fehyppl27IPkmCkdPOkHlI6+9McAwiAgl9PHf7bIA/zc7aBYTTTMuFjcGZ3fKcDj3imAjQATgA7gYgkTAMAuAFgJoMxQ94AuTAAgzmB7gElbAEA0bQIA6LcDrAKQZqgR0IUJAOBSQB8B9b0ALcUDeNr/eQBcVAqAWg0cAK+2+UoFOOrr233W6ufHfVaB/xG+dIAurPkIUHoEpMkIQJMRcGYjEGAyAgFmIxBgMgIBJiMQYDbC9ypAH0HfxPW3kT+k5X/K/mFa/zjTCLTwK402qNVf6/qLRX+1fX79AxhZZ/jFSMPEAAAAAElFTkSuQmCCLyogIHx4R3YwMHxiYWU3ZjFjNWE0NzU5ZDAyYTkxOWM4YTgwNzk3MzZiMSAqLw==) no-repeat;
            }

        .cpm-dialog-load .dialog-bd {
            padding: 45px 0;
            font: 14px/1.5 "Microsoft YaHei",Helvetica,sans-serif;
        }

        .cpm-dialog-load .icon-load {
            display: inline-block;
            height: 32px;
            width: 32px;
            margin-bottom: 20px;
            vertical-align: top;
            background-size: 32px auto;
            -webkit-animation: dialogLoading 1s linear infinite;
        }

        #orientation_hinter, .cpm-dialog-modx {
            position: fixed;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
        }

        @-webkit-keyframes dialogLoading {
            0% {
                -webkit-transform: rotate(0);
            }

            100% {
                -webkit-transform: rotate(360deg);
            }
        }

        .cpm-dialog-modx {
            right: 0;
            bottom: 0;
            z-index: 100;
            display: -webkit-box;
            display: -ms-flexbox;
        }

        #orientation_hinter {
            background: #131313;
            z-index: 999;
            display: none;
        }

            #orientation_hinter .hinter_phone {
                position: absolute;
                top: 50%;
                left: 50%;
                margin: -30px 0 0 -57px;
            }

                #orientation_hinter .hinter_phone i {
                    display: block;
                    width: 114px;
                    height: 60px;
                    background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOQAAAB4CAMAAADVLa6HAAABCFBMVEUAAAAAAAAAAABWVlYAAADFxcUAAACKioovLy8AAAAAAAAAAAC+vr6+vr6EhIQAAAALCwsBAQEGBgbIyMjGxsYAAADGxsbHx8cAAADHx8fAwMCoqKi9vb1SUlIGBgbHx8fFxcW0tLSxsbGgoKC7u7slJSVeXl6ysrLFxcW/v7+7u7uoqKiRkZEUFBQFBQWMjIxxcXHExMTExMTCwsK6urq2trbBwcHExMSsrKzExMS1tbWUlJTDw8O0tLSnp6dpaWm9vb1ZWVmDg4PAwMCvr6+Ojo4+Pj4xMTFKSkpnZ2e2trahoaGxsbGfn5+3t7ecnJyvr6+CgoKVlZU9PT1WVlasrKy8vLzJycmXdzvDAAAAV3RSTlMAARMjBL8WfEggDinGSGgeBwsj++IU8vkb9N6oeE0x7tBMt49nLykPxtLEmoM9NxcG6dvl0Ma6sLCnnYmHb11cW1hSUUFAOzo2NCQcqpmOfXx1bUY9Ny21pUe+AAAC6klEQVR42uzaV2/bMBSGYVI9kRs6aanhWc/YiffKHs1oM5t0r+///5MyrgvXqR0XuZIP+N5IF7rQAxIHAkQxTpJWDo+URySmRPri48oghcUvKDbef/UT3j9Mqaub4FSydePqSSb9KAGZtU9v3208W/g2Lk8OQ8Ns+4r+NlZTSFbWXaWJSC58BuGdlIC1dYfGxhywc+V6JPhEOhdgZ6yUZ8CruMOJOGSWM1iLqxHrNmOMiptRCFkOcOTr3/e72C6wW8ehLIfkN3cou0DqTZqjUYh+yexRT5j2sB/3pGDZKjKdBAlRC8B1IYWQm4gtG9w5sgVn2kKS5yxAcwbmazNgvfvLvrlMaQmL0JWSj+/XbEEJcYCKrxcX+dx9FHmLVNcRYhftWcj2ddS/ZOcia0AnIY0ltjwDGfO1jHZzkcI8MULSDORy1KfufyFdi7TISGSRFmmR0coiLdIio9UTkMMfPh5Jzkiqhkg1vyeIM/IcK63DTCOuJGPkXlhIe8c4dYkxcmklrsQqXry0yEhlkRZpkdHKIi3SIqOVRVqkRUYri+SJJK2U4zhKE1sk9Y6bYRAMmpUb1yOeyH6uiFGp1rpDkh9S3tWBbMWc17383DB3eVdLdsi7EMmY72gy6XKIYt4lbsh+HVv59J+BI3slZK8TxAyZQzKfpvGc7YX3JwclK2StiCPfmMaVEeRdYoU8w1Y3MfGuVMcHX7NCHgyPtU5UxXZBsULW8eXhq/7EoOuwQoY4fXhkt4agwwv5q707OGEYiKEg2kUgpyQtpBn334nB+OgChN78DgYWe1eg+Z8nyGMZ5PfpuL6WHVfiw0P8QojLAHGtMy7oxFPLeDQT4497kPV/7x5kGSNJZLgcZJCjE2SQQc5KkEEGOStButsExF4IseFD7GoZW3dBjkyQQQY5K0EGGeSsBClCrle6EXI+QrNICDMJ9SkhsV2tI/5dOmJFLE0owgnZu6HtJwoYjCoNoxTFqLcxiop2V06dXT9t25ovKx4AAAAASUVORK5CYII=) center center no-repeat;
                    background-size: 114px 60px;
                    -webkit-animation: rotateHint 2.6s ease-in infinite;
                    animation: rotateHint 2.6s ease-in infinite;
                }

            #orientation_hinter.portrait .hinter_phone {
                -webkit-transform: rotate(90deg);
                transform: rotate(90deg);
            }

            #orientation_hinter .hinter_rotate {
                width: 155px;
                height: 30px;
                position: absolute;
                top: 50%;
                left: 50%;
                margin: -15px 0 0 -77px;
                background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAATYAAAA8CAMAAADbjmZJAAAB71BMVEUAAAAICAgDAwPJyckEBAQCAgIGBgYEBAQDAwMBAQECAgIBAQEICAgAAAADAwMAAAAHBwfIyMgDAwPJycnGxsbIyMgUFBTFxcWgoKCMjIxnZ2dRUVECAgKNjY2ZmZnDw8MFBQUsLCyQkJDHx8fIyMjFxcXGxsaoqKjCwsLBwcFGRka/v78gICDExMTLy8vJycnCwsLBwcHHx8e9vb24uLjPz8/IyMi3t7fGxsbAwMC6urqysrKxsbHDw8PHx8fHx8eSkpKoqKh4eHjMzMy9vb1fX183NzcbGxsNDQ0VFRWrq6uDg4POzs7FxcW6urqoqKizs7Ourq66urrDw8PExMSQkJCysrJzc3O/v7+mpqbOzs6goKBISEi4uLg3NzcmJiZ0dHSMjIy6urq6urrMzMzc3Ny/v7/Jycm+vr67u7utra3ExMSpqanIyMjOzs6+vr6ioqKenp6jo6Ozs7Opqam3t7fS0tKqqqrBwcGFhYWhoaF5eXmJiYljY2Nvb2+Pj480NDTDw8Ofn59dXV0HBwfFxcVqampHR0dFRUWhoaGqqqqjo6O2trbb29uWlpaSkpK3t7dzc3PHx8ecnJxNTU2EhIS/v78PDw+ZmZlTU1MsLCzJycnKysrLy8vIyMjOzs7AwMDR0dG9vb2qqqqtra280xgSAAAAm3RSTlMABRD9CgIIIwwDKBkVHBMGDvoe+vb0MvKhg2ZaIAWZPS4tC/jl3LKviYNTTkH+9vHq5ODd2NbV0s/OxcW7uqiikY9/fn1fQDo2NTIjxL+7uLWurJePi4huZVBPSD44NSMhHBT8+vnz7unc08i+uLa0r62kmpaNg35xZWJcWlpYSEdGQT86MjElGg/w4t7Twq+jnJp/fXtcUykUCLZFqxcAAASkSURBVHja7ZxXWxNBFIZnd8MmkJAeEEkEAigCoiCgIBawIjZQUERp9t577713v9ml+UOdSWIIYeOT3J/3CpK79zllzpzdsAT5uiefEfNRbDaFZWK85vZzl+rQlTxGpBI50/nE5cgkrk7zVZ7s6Lz7Qqojd7OUab7gEiHOWkkNByDUFTf3frkz7HfrZC7OXgC+4gUVqs4seLS+dcowOOeaVhk8/qbLqTBCssPgUtypO06bVeHzOkeGNi2u1rhk5pPdwwiJOnhOitOKNxR6rXum4lCfd0xPTy7jM1/DFG0J8t2PdxgAr7xoVzNIGVwZNJdNTk4f+eFkRFKc6+ZRAIGzFaplvF09wQHDNM23T1VGzGLb3iIT9azdwlvkQgPAp9p6gd0FDkakMnpeertk96afMF7t1ACjoaT7AtBv1xkxh5fSW6DEb0uLNWltorXWr1QBmwupI8zztkN4K76XdjJbLK3tqXd7xjQEn7oYkU5PE8BXlaupaXpNWuvdKlpsHdBcRB3BgjKf8LYhnJKmD0KAsUdaY0uBJdQRrMi7LtN0iyvZTaMtwmNbvVvm7VqghDqCJWqr0LTAngy3fQCW17oUJghxXuqkqzdLDiI13Hpkinb5dSYYf3dsikpbBpQzsrr9y8X14p/2oniLUCoe3ndRjv4n3FaWq0wyGhLaSlMOJJSimdCrAC2haiOANQVeRmQmtQssijeFJsBY6Ke5IBvKAKwqlyF2WOZrN3WBrPh9Aghuc8s1QiLuiGzYCSB2QFsN8FraHeRQ3L6FPSzqA1aIsCOy4kAsN3X2TPbRchpCs6QOwOUCG/sOYEEBlbYcWqm86lgKmt1z4BniOwO5dt5Pp7Zco2096BI8B+oSNW2d1Ebnj9w6qS0WbQsp2rKlRraCsE61LTcWA1wkJ3XSnIhUAZX1LpGsdG7Lge0ATsnb78OxKYFu27JjU2JIYNEAzaTZI+89RANN/FVKrTTrHF3R7WaCGipuWbNudlEqi1vjNpWecc4i2HyArzaxRW6S+eqnh3Wt0bcMjzjiopRzwlSH3cEE8c1Ve7mXws2SsmMtV/y25JY0kNyMjoXk6qqQws2SmzBWxR8l6qkC+EURbAk+A2jrpuqWqQssijVMVc5VK1IeOBpbDhj9tLyypDoxstvkY1qBrkKdJZFzaUOpk9J0Pq9MaA9decy7UVjjl8IONku0VXxUXe+iM+88HgGNYgZ1xaytqZhbyR7LNG3fqpI3iyG0o8DxYp0B0RoOuZW0bwPi4z3kzep+bUPRgxYZa83p1gR9mvC2q95F9W0uTabZdWWCg2unLayxyFrprbo2bKNzSArj4h20YgPgod0VlqkYld740ZIit4fEJakzTc4B38n+TO9FRvoaAEy03S8gcUmWmhCYzffCwloGBkQ/BZ9ov2WnATXBWlMw+fr9olvD9kK3Q7H0MlQdiIm7SgNqgtWhZUdm/kybfGKqalfn3RGHpbfotZAGaLQ3/Ufk9pIPp48HA5os/IbRmUnMz49BrZFeTJglX9d/Dd3oWxn0CXWZ4ymvZ+CGk9amae48ttHBgV2NcrjKhKLTr4BYkOexeZM/P/MX0zZwfJeQztgAAAAASUVORK5CYII=) center center no-repeat;
                background-size: 155px 30px;
            }

        body, html {
            height: 100%;
        }

        #orientation_hinter .hinter_text {
            position: absolute;
            right: 0;
            bottom: 15%;
            left: 0;
            color: #bbb;
            font-size: 14px;
            font-weight: 400;
            text-align: center;
            line-height: 1;
            display: none;
        }

        @-webkit-keyframes rotateHint {
            0% {
                -webkit-transform: rotate(0);
                transform: rotate(0);
                opacity: .2;
            }

            50% {
                -webkit-transform: rotate(90deg);
                transform: rotate(90deg);
                opacity: 1;
            }

            100% {
                -webkit-transform: rotate(90deg);
                transform: rotate(90deg);
                opacity: 0;
            }
        }

        @keyframes rotateHint {
            0% {
                -webkit-transform: rotate(0);
                transform: rotate(0);
                opacity: .2;
            }

            50% {
                -webkit-transform: rotate(90deg);
                transform: rotate(90deg);
                opacity: 1;
            }

            100% {
                -webkit-transform: rotate(90deg);
                transform: rotate(90deg);
                opacity: 0;
            }
        }

        @media screen and (min-aspect-ratio:13/9) and (orientation:landscape) {
            #orientation_hinter.landscape, #orientation_hinter.landscape .landscape {
                display: block;
            }
        }

        @media screen and (max-aspect-ratio:13/9) and (orientation:portrait) {
            #orientation_hinter.portrait, #orientation_hinter.portrait .portrait {
                display: block;
            }
        }

        html {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
            font-size: 13px;
        }

        body {
            min-width: 320px;
            font-family: 'Helvetica Neue',Arial,'Hiragino Sans GB',STHeiti,'STHeiti Light [STXihei]','Microsoft Yahei',sans-serif;
            background-color: #eee;
            color: #50443f;
            line-height: 1;
        }

        * {
            margin: 0;
            padding: 0;
        }

        a img, img {
            border: 0;
        }

        a {
            color: #e02513;
            text-decoration: none;
            outline: 0;
        }

        ul {
            list-style: none;
        }

        h1, h2, h3, h4, h5 {
            font-weight: 700;
        }

        h1 {
            font-size: 17px;
        }

        h2 {
            font-size: 16px;
        }

        h3 {
            font-size: 15px;
        }

        input[type=search]::-webkit-search-cancel-button, input[type=search]::-webkit-search-decoration, input[type=search]::-webkit-search-results-button, input[type=search]::-webkit-search-results-decoration {
            display: none;
        }

        footer {
            padding: 20px 0;
            color: #b9b9b9;
            font-size: 12px;
            line-height: 1.6;
        }

            footer .f_logo {
                display: inline-block;
                width: 69px;
                height: 18px;
                text-indent: -9999px;
                background: url(http://mat1.gtimg.com/gongyi/m/wx/201699/foot_logo.png) no-repeat;
                background-size: 69px 18px;
            }

        .mod {
            background-color: #FFF;
            margin-top: 18px;
            /*overflow: hidden;*/
        }

            .mod .mod_title {
                position: relative;
                padding: 0 12px;
                line-height: 3;
            }

                .mod .mod_title .a_more {
                    position: absolute;
                    right: 12px;
                    top: 16px;
                }

        .recSlider {
            margin-bottom: 18px;
        }

        .rec_list {
            width: 100%;
            padding: 0 12px;
            vertical-align: bottom;
            box-sizing: border-box;
        }

            .rec_list .rec_list_inner {
                display: block;
                color: #50443f;
                background-color: #f8f8f8;
            }

            .rec_list .img_wrap {
                position: relative;
                width: 100%;
                padding-bottom: 50%;
                background-color: #aaa;
                overflow: hidden;
            }

                .rec_list .img_wrap:after {
                    display: block;
                    position: absolute;
                    left: 0;
                    bottom: 0;
                    width: 100%;
                    height: 50%;
                    background-image: url(data:data/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAYAAAC9CAYAAACQwQz+AAAAWUlEQVRYw+2XwQkAIAwDE3H/mV1AQUHBhutXyCVtoWhJ1qSaFnXxoafDYexLdZeymyL1p10mWCWHS+UAXgSeYvf8qv3Z3fj1IeDLbWceBMwLaLoLnC/AsdQARbECXbAm//wAAAAASUVORK5CYII=);
                    background-size: auto 100%;
                    background-position: 0 0;
                    background-repeat: repeat-x;
                }

                .rec_list .img_wrap img {
                    position: absolute;
                    left: 0;
                    top: 0;
                    width: 100%;
                }

                .rec_list .img_wrap .rec_name {
                    position: absolute;
                    left: 10px;
                    bottom: 10px;
                    color: #FFF;
                    z-index: 1;
                }

            .rec_list .desc {
                position: relative;
                padding: 0 110px 0 10px;
                margin: 8px 0 13px;
                line-height: 1.4;
                min-height: 36px;
                white-space: normal;
                overflow: hidden;
                text-overflow: ellipsis;
                display: -webkit-box;
                -webkit-line-clamp: 2;
                -webkit-box-orient: vertical;
            }

        .notice .notice_marquee span, .rankboard_head .p1, .suprise_link a {
            overflow: hidden;
            white-space: nowrap;
        }

        .rec_list .desc .btn_help {
            display: block;
            position: absolute;
            top: 2px;
            right: 10px;
            font-size: 15px;
            line-height: 1;
            padding: 6px 10px;
            border: 1px solid #e02513;
            border-radius: 60px;
        }

        .rec_list .progress {
            padding: 0 10px 20px;
        }

            .rec_list .progress .prog_bar {
                margin-bottom: 5px;
                width: 100%;
                height: 4px;
                background-color: #dcc6be;
                border-radius: 4px;
                overflow: hidden;
            }

                .rec_list .progress .prog_bar .prog_done {
                    height: 100%;
                    background-color: #e02513;
                }

            .rec_list .progress .prog_info {
                color: #88746c;
                overflow: hidden;
            }

                .rec_list .progress .prog_info .ft {
                    float: left;
                }

                .rec_list .progress .prog_info .rt {
                    float: right;
                }

        .adSlider .swiper-slide a, .ceoSlider .swiper-slide a, .donateSlider .swiper-slide a {
            position: relative;
            display: block;
            width: 100%;
            height: 0;
            overflow: hidden;
            background-color: #DDD;
        }

            .adSlider .swiper-slide a img, .ceoSlider .swiper-slide a img, .donateSlider .swiper-slide a img {
                position: absolute;
                width: 100%;
                top: 0;
                left: 0;
            }

        .donateSlider {
            margin-bottom: 18px;
        }

            .donateSlider .swiper-slide a {
                padding-top: 42.6%;
            }

        .adSlider .swiper-slide a {
            padding-top: 26.6%;
        }

        .ceoSlider .swiper-slide a {
            padding-top: 37.6%;
        }

        .ceoSlider_wrap {
            margin: 0 10px;
        }

        .notice {
            padding: 10px 12px 10px 34px;
            background-color: #FFF;
            background-image: url(data:data/png;base64,iVBORw0KGgoAAAANSUhEUgAAABoAAAAWCAMAAADpVnyHAAAAwFBMVEX+9vbiMiH86+r86ef639zjOSn3yMTzrKXuh33gJRPkPi7//f3gKBf++/r//PvhKhnhLx7++fnjPCztgHXlRzjhLBr+9vX99PPjOCf0savwlo32vLfnWEvrdWn51dLyoJj75OHkRTX2wLv4zcn98/L74N70r6nvkIfyp6D508/gJxX1t7H98O72vrn52NT3ysblSzzmUkToXU/oYFPpZ1rqaVz+9fTqbmLuioDvj4b85+XypJz0tK7++Pj3xcD///+Zi/V2AAAAQHRSTlP///////////////////////////////////////////////////////////////////////////////////8AwnuxRAAAANJJREFUKM910tdSAzEMhWEvCUn+tbelLjWE3nsNkPP+b8UF7NjZMbqS5hvdHMkoqIv6LJiMb7MRjKN0WwOpJOU9G1I+gIbmLDY8dYd4mg0pOn+UfUBAek3ZzGV04/CVSrJWHTiXUUmLHnet+pQyok0P9JU7lhHap5Ze6EXIgNU7OxGaUUhPrCJ0ykCZ48AoadGU4lhfOGt0VLe2rpa6dmz/plHtradRjUm+mwzvyoCqey67Pnnz5umT5GTtXqtJQ2Yra115+gyL+ANIh5PRfyQbDj8VTFM6vTsHbQAAAABJRU5ErkJggg==);
            background-size: 13px 11px;
            background-position: 12px 12px;
            background-repeat: no-repeat;
            overflow: hidden;
        }

        .a_match_rule:after, .a_more:after {
            margin-left: 3px;
            background-size: 13px 13px;
            background-position: right top;
            background-repeat: no-repeat;
            vertical-align: -2px;
        }

        .notice .notice_wrap {
            width: 100%;
            overflow: hidden;
        }

        .notice .notice_marquee {
            position: relative;
            height: 15px;
        }

            .notice .notice_marquee span {
                position: absolute;
                left: 0;
                top: 0;
                display: block;
                line-height: 1.2;
                padding-right: 100px;
                padding-left: 20px;
            }

        .a_more, .tap_more {
            position: relative;
        }

        .notice .notice_marquee.marquee span:first-child {
            -webkit-animation: a-marquee-notice1 20s linear both;
            animation: a-marquee-notice1 20s linear both;
            -webkit-animation-iteration-count: infinite;
            animation-iteration-count: infinite;
        }

        .notice .notice_marquee.marquee span:last-child {
            -webkit-animation: a-marquee-notice2 20s linear both;
            animation: a-marquee-notice2 20s linear both;
            -webkit-animation-iteration-count: infinite;
            animation-iteration-count: infinite;
        }

        @-webkit-keyframes a-marquee-notice1 {
            0% {
                opacity: 1;
                -webkit-transform: translateX(0);
                transform: translateX(0);
            }

            49.9% {
                opacity: 1;
                -webkit-transform: translateX(-100%);
                transform: translateX(-100%);
            }

            50% {
                opacity: 0;
                -webkit-transform: translateX(-100%);
                transform: translateX(-100%);
            }

            50.1% {
                opacity: 0;
                -webkit-transform: translateX(100%);
                transform: translateX(100%);
            }

            50.2% {
                opacity: 1;
                -webkit-transform: translateX(100%);
                transform: translateX(100%);
            }

            100% {
                opacity: 1;
                -webkit-transform: translateX(0);
                transform: translateX(0);
            }
        }

        @keyframes a-marquee-notice1 {
            0% {
                opacity: 1;
                -webkit-transform: translateX(0);
                transform: translateX(0);
            }

            49.9% {
                opacity: 1;
                -webkit-transform: translateX(-100%);
                transform: translateX(-100%);
            }

            50% {
                opacity: 0;
                -webkit-transform: translateX(-100%);
                transform: translateX(-100%);
            }

            50.1% {
                opacity: 0;
                -webkit-transform: translateX(100%);
                transform: translateX(100%);
            }

            50.2% {
                opacity: 1;
                -webkit-transform: translateX(100%);
                transform: translateX(100%);
            }

            100% {
                opacity: 1;
                -webkit-transform: translateX(0);
                transform: translateX(0);
            }
        }

        @-webkit-keyframes a-marquee-notice2 {
            0% {
                opacity: 1;
                -webkit-transform: translateX(100%);
                transform: translateX(100%);
            }

            50% {
                opacity: 1;
                -webkit-transform: translateX(0);
                transform: translateX(0);
            }

            99.6% {
                opacity: 1;
                -webkit-transform: translateX(-100%);
                transform: translateX(-100%);
            }

            99.7% {
                opacity: 0;
                -webkit-transform: translateX(-100%);
                transform: translateX(-100%);
            }

            99.8% {
                opacity: 0;
                -webkit-transform: translateX(100%);
                transform: translateX(100%);
            }

            100% {
                opacity: 1;
                -webkit-transform: translateX(100%);
                transform: translateX(100%);
            }
        }

        @keyframes a-marquee-notice2 {
            0% {
                opacity: 1;
                -webkit-transform: translateX(100%);
                transform: translateX(100%);
            }

            50% {
                opacity: 1;
                -webkit-transform: translateX(0);
                transform: translateX(0);
            }

            99.6% {
                opacity: 1;
                -webkit-transform: translateX(-100%);
                transform: translateX(-100%);
            }

            99.7% {
                opacity: 0;
                -webkit-transform: translateX(-100%);
                transform: translateX(-100%);
            }

            99.8% {
                opacity: 0;
                -webkit-transform: translateX(100%);
                transform: translateX(100%);
            }

            100% {
                opacity: 1;
                -webkit-transform: translateX(100%);
                transform: translateX(100%);
            }
        }

        .a_more {
            line-height: 1;
        }

            .a_more:after {
                display: inline-block;
                width: 13px;
                height: 13px;
                background-image: url(data:data/png;base64,iVBORw0KGgoAAAANSUhEUgAAABoAAAAaCAMAAACelLz8AAAAwFBMVEXjOir3x8PvjIL1urTiNCP////gJRP98fDtgHbgJxXhLBr++vntgHXseW7hLx7mTj///PziMB///v763tvkQDH+9/f4zcj98vHjPS32vbj0savuhnz85+X40Mz75uT75ePsd2zrdGjrdWn62dXuiH7++/r40c30r6j+9fT4z8v2vrn99PPwlIvse3DmTD7tfXL75OL52NT0sKrhLhzuhXv639zvjYT629j74d/50s7lSTr4zMftgXfxnpbxmZH///8dCsodAAAAQHRSTlP///////////////////////////////////////////////////////////////////////////////////8AwnuxRAAAARpJREFUKM+F0mlzgjAQBuBoyRsICNajiojY1vuutvYu//9fSYKEpdOZ7heyeRjYbJal13DjSeD7wSR2ix2WP4QNE7agNHBg2WHirtuhbcEZlLSF3Cz4NRYbiW1BDXSeOInHDho5zdEfZhuH81thwz7mioQjY5V7YO3CYumIjDzUdfp+S6wOL2VdWL08vSPWs9BlNYyLV6l5qLEIN/wPC/HMAox4xV71aoSA+XDJkT4dfOiFCz+jdSmtrJVjQwFeKvLQNB8kZRDRZZTFK7lvkuLNkVtfRPSRTaO+ieSNUu2dqezH/IfzWd7e9JhfCj9wcinH/65SmayLAsRZaqFjs0qm02T1a2yyYduXw7avDJsa0WW0O5120dKM6AXTQjdJ59od8wAAAABJRU5ErkJggg==);
            }

        .match_info:before, .tap_more:after {
            right: 0;
            top: 0;
            width: auto;
            height: 1px;
            color: #ddd;
            -webkit-transform-origin: 0 0;
            -webkit-transform: scaleY(.5);
            left: 0;
            border-top: 1px solid #ddd;
            content: " ";
        }

        .a_more.a_more_down:after {
            background-image: url(data:data/png;base64,iVBORw0KGgoAAAANSUhEUgAAABoAAAAaCAMAAACelLz8AAAAwFBMVEXjOir3x8PvjIL1urTiNCP////gJRP98fDtgHbgJxXhLBr++vntgHXseW7hLx7mTj///PziMB///v763tvkQDH+9/f4zcj98vHjPS32vbj0savuhnz85+X40Mz75uT75ePsd2zrdGjrdWn62dXuiH7++/r40c30r6j+9fT4z8v2vrn99PPwlIvse3DmTD7tfXL75OL52NT0sKrhLhzuhXv639zvjYT629j74d/50s7lSTr4zMftgXfxnpbxmZH///8dCsodAAAAQHRSTlP///////////////////////////////////////////////////////////////////////////////////8AwnuxRAAAASNJREFUKM9t0ul2gjAQBeBomcsqWJcqImJb973a2r15/7cyCYRFO38C+c6Byc0wnpUbTwLfDyaxq3dYupgW8rLMMg1sGFaYuOt2aBmwBwVt4WwWlNVi42CrqYHOC9fCOT130Ehpjv6QykTDPuaSTNuJqUoUO7YpyEOdbqoOj7MujN4t9Qx0WQ1j8fj328q3P70DkYcai3BHdAB+tD3c40wU4pUFGIn3JxFCSwv7IBohYD5csdF8zExKW6wufEFrKkyLogBvlNu3FvVB1YYy8T9bi2pDNs9z06Kal0fOUmqOv97LR86CqmaYBiXjnV3TLI2XH/+7lGNxleVsi6uU5tRNDebZUVIem1UynSarq7ERw7Yvhm1fGTY5ostodzrtomU+ohdPpjvRph0DzwAAAABJRU5ErkJggg==);
        }

        .tap_more {
            padding: 14px 0;
        }

            .tap_more:after {
                position: absolute;
                transform-origin: 0 0;
                transform: scaleY(.5);
            }

        .match_info {
            position: relative;
            padding: 20px 0;
            line-height: 1.2;
        }

            .match_info:before {
                position: absolute;
                transform-origin: 0 0;
                transform: scaleY(.5);
            }

            .match_info .num {
                color: #e02513;
                padding: 0 4px;
            }

            .match_info .match_p1 {
                font-size: 15px;
                color: #88746c;
                font-weight: 700;
            }

            .match_info .match_p2 {
                margin-top: 8px;
                font-size: 16px;
                color: #88746c;
                font-weight: 700;
            }

                .match_info .match_p2 .num {
                    padding: 0 2px;
                }

            .match_info .match_p3 {
                margin-top: 13px;
                font-size: 13px;
                color: #50443f;
            }

        .a_match_rule {
            position: absolute;
            right: 12px;
            top: 16px;
            color: #e02513;
            line-height: 1;
        }

            .a_match_rule:after {
                content: " ";
                display: inline-block;
                width: 13px;
                height: 13px;
                background-image: url(data:data/png;base64,iVBORw0KGgoAAAANSUhEUgAAABoAAAAaCAYAAACpSkzOAAACsklEQVRIx8VWTWgTQRROL4XeCkpBr9qitAqK2iIqbaFH8eDBQ0oP9iL4d7J6rHozVcG2UioREUT0oAcVrKA3EVGJUSsq/qT5bWvaJtlEu7uZPOfNzoZJdnZnI4IPHuzOft/7Zmfem3kBAAh4eax9TTP1buoh6hHqGvcIH8Nvzao4XgKt1Ceog09HbKtvIQpGD1InGCC+YwMsjY7Ar6ePwUwloGLoQIoaGN++sDH8hhguRjjXW4gCmqhPI2muaz3kJkJQKRVBZYhBLHK4IMZokgrxPwkjMNm7DfRPH6BRMz7PMi4XC4t/JgoNISCxdwuUMyn5zHUdzHgMygsZVzHkYgwuNlQjxDee4K+vvn3tJM+nYfHkMMx1rqtufrJ/OxTv35GKYQy+jMROEFtoEskrY+ccJJJbFpfD4drdm1Kx5dBZGzPJhOhDi51dpJB3EHCT2ZLu6YLfL59DxTTAjH2H+eB+a3z3Zjob4pwgjSVkYwsK9eFLduSodGaZgwMMXLh1vWZc//i++lfl7E8pd2n0lI3pQ6FL+FJ68lAKTg3sYjPTZ9/VjGNS2EJEK0i5WGcccznAjxIw00nfaYxFuzB8iAVJH+h1xWFMLhQJ8HOLkX2LHAlaATa1weqrF55YLqRVhfBY8WOLJw5b5I61ULh9QzkpUYgtnfHjq1Kk+OhedV9y4xeU+Pqls5Jh5oGSmD1znBGxcDHNVVafDFZ6nz4G/9qE9O4XCnajtGBrivfqRVa4WMQqcxSs6ggSLdHdwYjxne1KIYxVcwQ5DtXoG1dy/to4JPdthfz0FU8RjCE9VLnYoOqa8GN118Tg/7v4hKs8bF/luM4kn1NvPMWsjJ0Xr/Kw61WubE6ezVjNCa0fdlXQZxz7q+ZE0m5NNdBuTTXUbrk0kD38BIlSL3GP8rEePw3kH0p5KB2Scy0hAAAAAElFTkSuQmCC);
            }

        @media screen and (max-width:361px) {
            .match_info .match_p1, .match_info .match_p2 {
                font-size: 14px;
            }
        }

        .suprise_link {
            position: relative;
        }

            .suprise_link:before {
                content: " ";
                position: absolute;
                left: 0;
                right: 0;
                top: 0;
                width: auto;
                height: 1px;
                border-top: 1px solid #ddd;
                color: #ddd;
                -webkit-transform-origin: 0 0;
                transform-origin: 0 0;
                -webkit-transform: scaleY(.5);
                transform: scaleY(.5);
            }

            .suprise_link a {
                position: relative;
                display: block;
                width: 100%;
                padding: 11px 25px 11px 15px;
                line-height: 1.5;
                color: #50443f;
                box-sizing: border-box;
                text-overflow: ellipsis;
            }

                .suprise_link a > div:before {
                    content: " ";
                    display: inline-block;
                    width: 66px;
                    height: 18px;
                    background: url(http://mat1.gtimg.com/gongyi/m/wx/201699/suprise_text_rd.png) left center no-repeat;
                    background-size: 66px 13px;
                    vertical-align: -4px;
                    padding-right: 12px;
                    margin-right: 5px;
                    border-right: 1px solid #E0E0E0;
                }

                .suprise_link a > div b {
                    color: #50443f;
                }

                .suprise_link a > div .hot {
                    display: inline-block;
                    vertical-align: bottom;
                    margin: 0 6px 0 4px;
                    padding: 1px 4px;
                    background: #E45E5E;
                    color: #FFF;
                    font-size: 12px;
                    border-radius: 2px;
                    -webkit-transform: scale(.8);
                    transform: scale(.8);
                    -webkit-transform-origin: center center;
                    transform-origin: center center;
                }

                .ngo_title p:after, .ngo_title p:before, .rankboard_head .a_rule:before, .suprise_link a:after {
                    content: " ";
                    background-repeat: no-repeat;
                }

                .suprise_link a:after {
                    display: block;
                    position: absolute;
                    right: 8px;
                    top: 50%;
                    width: 9px;
                    height: 15px;
                    margin-top: -7px;
                    background-image: url(data:data/png;base64,iVBORw0KGgoAAAANSUhEUgAAABIAAAAeCAQAAACLBYanAAABHklEQVQ4y4XTIUuDURjF8Wd7dTp0YcpgRbZgUkEE0eKiTdBgFYtBMOgHMAwsA/0AFsuSmORfZAbBZFPGmmlJZjDJksgxjJeJ7Hnk1h/cy7nnGDNYdGQyo8fG/6iP2CYTo1XeEEdkI2RUaCPOyUXIKNJC3DIdISPPFeKFUoSMMeqId+YjZGQ4RHyzFiHD2KGP2BpGMgoZ6/QQB2kko5FRpYNoDCLxkDHLPeKGqQilkTxRipCRUEd0qUbI2EN8suKjhBNElyX/ugkuEM/M+Q8vcI24o+hHUOYRccmkH+YCr4hTEv9banwg9n+X+S/aRXyx6VclyzGix7JfuhwNRJuKX98CTURr9EwHqMwDokk+mlQHccZ4PM5gmEO06E88RT+lFszIGsCx+AAAAABJRU5ErkJggg==);
                    background-size: 9px 15px;
                    background-position: right center;
                }

        .tab .tab_hd {
            padding-top: 15px;
        }

            .tab .tab_hd a {
                display: inline-block;
                margin-left: 14px;
                width: 26%;
                padding: 15px 0;
                color: #50443f;
                border: 1px solid #c2bebc;
                border-radius: 2px;
            }

                .tab .tab_hd a:first-child {
                    margin-left: 0;
                }

                .tab .tab_hd a.active {
                    background-color: #50443f;
                    border-color: #50443f;
                    color: #FFF;
                }

        .tab .tab_bd {
            overflow: hidden;
        }

            .tab .tab_bd .tab_content {
                display: none;
                overflow: hidden;
            }

                .tab .tab_bd .tab_content.active {
                    display: block;
                }

        .rankboard_head {
            position: relative;
            line-height: 1.2;
            padding: 14px 12px;
        }

            .rankboard_head .a_rule {
                position: absolute;
                right: 20px;
                top: 10px;
            }

                .rankboard_head .a_rule:before {
                    display: inline-block;
                    width: 13px;
                    height: 13px;
                    background-image: url(data:data/png;base64,iVBORw0KGgoAAAANSUhEUgAAACIAAAAiCAMAAAANmfvwAAAApVBMVEW0tLT+/v7s7OzCwsLAwMDZ2dnX19e8vLz6+vrz8/P9/f3Ly8vHx8e5ubne3t7j4+O4uLjk5OTDw8P29vbMzMzOzs7f39/y8vK2trbNzc3S0tLx8fHb29vW1tbQ0NC9vb3ExMTg4ODn5+f5+fnw8PC7u7u+vr7Pz8/V1dX8/Pz09PS6urr4+PjFxcXh4eHGxsbi4uLKysr7+/v39/fc3NzJycn///+3YSR6AAAAN3RSTlP///////////////////////////////////////////////////////////////////////8AEFmdiwAAAYNJREFUOMuNlNd23DAMRC9F9b5N3uJuJy6JnT7//2l5oAq1u+fYfBJwrkTMABD68OA9v3Ybm1SJ3XSvZxGzWDKd5cKcIEEIkPzbX+2LG4AwmCMmBr5EWZ+8uPwKxMZD8jVsL3PvfvNewTofEVNAeHGkY2WhMAMSw216ovWlgLhHAggfXDptoiiqVy7ISwgkIROyPTjguZd87b75/RuhkdACIveaHW2xLtHAQkK3VL8lSRG0dSo1LdSOCVlKZHDl4hYaZyOULnUHmejgUZJ0gJte74g8QCc2VIMTQe95BIWG8jbC8nZkyP14o/SEFQm7OfHMeI8Uk4iWax9ILXiZv7QnXymgvZ/CmOS4lgO0By9+wvqKJKmexEyK6sGXQW/jVwad726P1B7i3NWS6mUapCDwJ8f1yOu0Q1YTMXTahGzHqSx9fpwXBRCmZ5Bp6mazG5XloChfT7PrNiA7auafH/4GnNujn/M9+sw2Tjv9ax/v12d3+lN/BknK6p1NqsTu6lnt/wGCb6Ns5HAfXwAAAABJRU5ErkJggg==);
                    background-size: 13px 13px;
                    background-position: right top;
                    vertical-align: -2px;
                }

            .rankboard_head b {
                color: #e02513;
                font-size: 16px;
            }

            .rankboard_head h2 b {
                font-size: 18px;
            }

            .rankboard_head .p1 {
                color: #88746c;
            }

                .rankboard_head .p1 span {
                    display: block;
                    float: left;
                    overflow: hidden;
                }

                .rankboard_head .p1 .lt {
                    width: 62%;
                    text-align: left;
                }

                .rankboard_head .p1 .rt {
                    text-align: right;
                    width: 38%;
                }

        .countdown, .mod.mod_end_info, .mod_QQmoney, .ngo_title, .statistics_col, .videolink {
            text-align: center;
        }

        .rankboard_head .p1 b {
            font-size: 16px;
        }

        .rankboard_head .p2 {
            margin-top: 16px;
        }

            .rankboard_head .p2 .funder {
                height: 14px;
                margin-right: 4px;
                vertical-align: -4px;
            }

        .ul_list {
            display: block;
        }

            .ul_list .list_item {
                position: relative;
                padding: 16px 12px;
            }

                .ul_list .list_item:before {
                    content: " ";
                    position: absolute;
                    left: 12px;
                    right: 12px;
                    top: 0;
                    width: auto;
                    height: 1px;
                    border-top: 1px solid #ddd;
                    color: #ddd;
                    -webkit-transform-origin: 0 0;
                    transform-origin: 0 0;
                    -webkit-transform: scaleY(.5);
                    transform: scaleY(.5);
                }

                .ul_list .list_item a {
                    display: -webkit-box;
                    display: -ms-flexbox;
                    display: flex;
                    width: 100%;
                    -webkit-box-align: center;
                    -ms-flex-align: center;
                    align-items: center;
                }

                .ul_list .list_item .pic {
                    padding: 0 18px 0 6px;
                }

                    .ul_list .list_item .pic .pic_wrap {
                        width: 44px;
                        height: 44px;
                        overflow: hidden;
                        border-radius: 100%;
                        background-color: #FFF;
                    }

                        .ul_list .list_item .pic .pic_wrap img {
                            width: 100%;
                            height: 100%;
                        }

                .ul_list .list_item .main {
                    -webkit-box-flex: 1;
                    -ms-flex: 1;
                    flex: 1;
                    line-height: 1.4;
                }

                    .ul_list .list_item .main .m_name {
                        color: #50443f;
                        margin-bottom: 8px;
                    }

                    .ul_list .list_item .main .m_p {
                        color: #88746c;
                    }

                        .ul_list .list_item .main .m_p .num {
                            color: #50443f;
                            padding: 0 3px;
                        }

        .ngo_title {
            padding: 20px 0;
        }

            .ngo_title h1 {
                color: #e02513;
                font-size: 17px;
            }

            .ngo_title p {
                position: relative;
                display: inline-block;
                margin-top: 6px;
                padding: 0 32px;
                line-height: 1.2;
                font-size: 15px;
                font-weight: 700;
                color: #d2c6c0;
            }

                .ngo_title p:after, .ngo_title p:before {
                    display: block;
                    position: absolute;
                    top: 4px;
                    width: 29px;
                    height: 30px;
                    background-size: 100%;
                    background-position: right center;
                    opacity: .7;
                }

                .ngo_title p:before {
                    left: 0;
                    background-image: url(data:data/png;base64,iVBORw0KGgoAAAANSUhEUgAAADoAAAA8CAMAAAAAPFkHAAAAwFBMVEXg2NT9/PzVysTo4t/z7+7s5uTXzMfq5OLk3Nnz8O/e1dDt6Obl3tvTx8Ht6OXYzcj49vXp4+D+/v7////8+/v59/bw7Or+/f3f1tHb0czUyML18vH6+fjn4N3r5uPq5OHVycP8+/rb0s3Zz8r29PLm4Nzj29f5+PfUycP08fDd1M/k3dnf1tL7+vn39PP7+vra0Mv18/Hj29jg19P28/Lu6efd1ND39fT8/Pvm39zu6uf08vD18vD49vb49/b////p4+XoAAAAQHRSTlP///////////////////////////////////////////////////////////////////////////////////8AwnuxRAAAAbZJREFUSMftldlywjAMRc0SSgGT4IQt7HsLhe774v//q0Kh6DqRyUzfOhM9MXNysCLJitDRUK91fSo+K2r/Q0RJ15Xy8oRZlXIyYtW83MWbTfSLOxzeMuqZ3EeON1XpwPsxdSZ/Y+4zZrN95EKZ6kpSPDXjZ7aBu2tUe0BkO4i9Zwv5xQOoDpJiPGHMSU5eIOEPJE78RRvIWwrKNAqBPMbNLprCxwqPgVSZhk7wTB/72gcyYPqChRgrnKZrzIYxR8CzGWOGr4gUFKPeg/qsUd0A8RizDvw4o3sVxqTHzS6b1I86JTLk0sXGTLWhlog0uEMrxJfaUNdQPfbCwKFfpjogUuYOzUNLtalmCXFvqpfE303VI7LizAzxUJkq5MvuwnOmpwe1QJ3RCfX1TDVIGAcdWkohdIdIhzMD28UQukYow6kd27wIfUPzwObr2KoofCJzVhX0gLlfRXByr2xjSPsz8p9eQpUUrKSICgO6SCjwLKKWCQWcurAuZ5GzVeEQU+tdFncJ1wYmOPLFFrAhNh5FwHwvHODbFgvc6twHoMpzd6u6f1cLCWrfrsoEtZaqqZqqqfqv1G/Ma9LBv4G9BQAAAABJRU5ErkJggg==);
                }

                .ngo_title p:after {
                    right: 0;
                    background-image: url(data:data/png;base64,iVBORw0KGgoAAAANSUhEUgAAADoAAAA8CAMAAAAAPFkHAAAAwFBMVEXy7+3r5ePl3tvf1tLe1dHZzsno4t/XzMfTx8H+/v7UyML////b0cz9/Pz39fT8+/vd08/18vHm4Nzu6efa0MvZz8rVysT7+vr7+vnVycPt6Obj29fi2tbv6+n49vXz8O/UycPWy8XWy8bq5OH8+/r+/f36+Pj29PLYzcjf1tH8/Pv9/fzv6ujh2dX6+fjb0s3az8rj29jj3Njg19P9/f3t6OXm39zk3Nnk3drx7ev5+Pf08fDx7uzg2NT59/b///+h6gkvAAAAQHRSTlP///////////////////////////////////////////////////////////////////////////////////8AwnuxRAAAAbBJREFUSMftl8lywjAMQFMKxSYJa9h3CmWnLd1Lqf7/rzh0sGRZCTMcO/H18WJZVkTkwdXLAwAlL/ObGJ6qqZqqqfq/1ML16iJeHV1Q6x7ry12DnsS+/Wl4lqtZg25F9Wj4M1frBu1FNTC8ylU8y42oFgzfMFWj+iWZhB+YOkU0l9QS8l+mzhE1JXWAfM3UGiItqX1SMUzdGpIXs4TXvuQqJrAqqiPDO0xt2e+Fs3bIi0wlR2lJKknFjKlYKw0x3sjwMtgqubXqhVvtM7VDjyKsN+RTW/XH2AKkWyU8AFsdXoh3aBc4VXOIHqRNF3ZQRPWUFY+zfpDfg6XqBqKKYM7ayENbfSfNTtp0j/wOLJW8buKmFdKB15YaknDLUjVg4asNUFU/qsT06og0YN9St4qlj5llwndA1a5yH0rNJeE1+hXu9+gfgtuT9JHgHv2An9Fo1NAxXwOCI03U74lKPOhgTPAkpGNDkVSJyrgpOhDcaNoTxypIMgEGptdNpnxY0R+JXRBa+T+cC50559yuOnHDUBgppdSLBkGFYlsVKvGDlM4KCTzX8KpXShrC/IzbrE5TwscDyl8/fwAAAABJRU5ErkJggg==);
                }

        .ngo_list_wrap {
            margin: 20px 0;
            overflow: hidden;
        }

            .ngo_list_wrap .list_item {
                display: -webkit-box;
                display: -ms-flexbox;
                display: flex;
                -webkit-box-align: center;
                -ms-flex-align: center;
                align-items: center;
            }

        .ngo_list_content {
            position: relative;
        }

            .ngo_list_content.marquee .ngo_list:first-child {
                -webkit-animation: a-marquee-ngo1 20s linear both;
                animation: a-marquee-ngo1 20s linear both;
                -webkit-animation-iteration-count: infinite;
                animation-iteration-count: infinite;
            }

            .ngo_list_content.marquee .ngo_list:last-child {
                -webkit-animation: a-marquee-ngo2 20s linear both;
                animation: a-marquee-ngo2 20s linear both;
                -webkit-animation-iteration-count: infinite;
                animation-iteration-count: infinite;
            }

        @-webkit-keyframes a-marquee-ngo1 {
            0% {
                opacity: 1;
                -webkit-transform: translateY(0);
                transform: translateY(0);
            }

            49.9% {
                opacity: 1;
                -webkit-transform: translateY(-100%);
                transform: translateY(-100%);
            }

            50% {
                opacity: 0;
                -webkit-transform: translateY(-100%);
                transform: translateY(-100%);
            }

            50.1% {
                opacity: 0;
                -webkit-transform: translateY(100%);
                transform: translateY(100%);
            }

            50.2% {
                opacity: 1;
                -webkit-transform: translateY(100%);
                transform: translateY(100%);
            }

            100% {
                opacity: 1;
                -webkit-transform: translateY(0);
                transform: translateY(0);
            }
        }

        @keyframes a-marquee-ngo1 {
            0% {
                opacity: 1;
                -webkit-transform: translateY(0);
                transform: translateY(0);
            }

            49.9% {
                opacity: 1;
                -webkit-transform: translateY(-100%);
                transform: translateY(-100%);
            }

            50% {
                opacity: 0;
                -webkit-transform: translateY(-100%);
                transform: translateY(-100%);
            }

            50.1% {
                opacity: 0;
                -webkit-transform: translateY(100%);
                transform: translateY(100%);
            }

            50.2% {
                opacity: 1;
                -webkit-transform: translateY(100%);
                transform: translateY(100%);
            }

            100% {
                opacity: 1;
                -webkit-transform: translateY(0);
                transform: translateY(0);
            }
        }

        @-webkit-keyframes a-marquee-ngo2 {
            0% {
                opacity: 1;
                -webkit-transform: translateY(100%);
                transform: translateY(100%);
            }

            50% {
                opacity: 1;
                -webkit-transform: translateY(0);
                transform: translateY(0);
            }

            99.6% {
                opacity: 1;
                -webkit-transform: translateY(-100%);
                transform: translateY(-100%);
            }

            99.7% {
                opacity: 0;
                -webkit-transform: translateY(-100%);
                transform: translateY(-100%);
            }

            99.8% {
                opacity: 0;
                -webkit-transform: translateY(100%);
                transform: translateY(100%);
            }

            100% {
                opacity: 1;
                -webkit-transform: translateY(100%);
                transform: translateY(100%);
            }
        }

        @keyframes a-marquee-ngo2 {
            0% {
                opacity: 1;
                -webkit-transform: translateY(100%);
                transform: translateY(100%);
            }

            50% {
                opacity: 1;
                -webkit-transform: translateY(0);
                transform: translateY(0);
            }

            99.6% {
                opacity: 1;
                -webkit-transform: translateY(-100%);
                transform: translateY(-100%);
            }

            99.7% {
                opacity: 0;
                -webkit-transform: translateY(-100%);
                transform: translateY(-100%);
            }

            99.8% {
                opacity: 0;
                -webkit-transform: translateY(100%);
                transform: translateY(100%);
            }

            100% {
                opacity: 1;
                -webkit-transform: translateY(100%);
                transform: translateY(100%);
            }
        }

        .ngo_list {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            padding-bottom: 50px;
        }

            .ngo_list .list_item:before {
                content: none;
                display: none;
            }

        .donate_list_title .a_rule:before, .lovers_list .list_item .main .m_name .location:before {
            display: inline-block;
            margin-right: 3px;
            background-position: right top;
            background-repeat: no-repeat;
            content: " ";
        }

        .ngo_list .list_item .pic {
            padding: 0 14px 0 6px;
        }

            .ngo_list .list_item .pic .pic_wrap {
                width: 56px;
                height: 56px;
            }

        .donate_list .list_item .pic {
            padding: 0 10px 0 0;
        }

            .donate_list .list_item .pic .pic_wrap {
                width: 95px;
                height: 72px;
                border-radius: 6px;
            }

        .donate_list_title {
            position: relative;
            padding: 15px 12px 0;
            overflow: hidden;
        }

            .donate_list_title:before {
                content: " ";
                position: absolute;
                left: 0;
                right: 0;
                top: 0;
                width: auto;
                height: 1px;
                border-top: 1px solid #ddd;
                color: #ddd;
                -webkit-transform-origin: 0 0;
                transform-origin: 0 0;
                -webkit-transform: scaleY(.5);
                transform: scaleY(.5);
            }

            .donate_list_title .a_rule {
                position: relative;
                float: right;
                color: #acacac;
            }

                .donate_list_title .a_rule:before {
                    width: 13px;
                    height: 13px;
                    background-image: url(data:data/png;base64,iVBORw0KGgoAAAANSUhEUgAAACIAAAAiCAMAAAANmfvwAAAApVBMVEW0tLT+/v7s7OzCwsLAwMDZ2dnX19e8vLz6+vrz8/P9/f3Ly8vHx8e5ubne3t7j4+O4uLjk5OTDw8P29vbMzMzOzs7f39/y8vK2trbNzc3S0tLx8fHb29vW1tbQ0NC9vb3ExMTg4ODn5+f5+fnw8PC7u7u+vr7Pz8/V1dX8/Pz09PS6urr4+PjFxcXh4eHGxsbi4uLKysr7+/v39/fc3NzJycn///+3YSR6AAAAN3RSTlP///////////////////////////////////////////////////////////////////////8AEFmdiwAAAYNJREFUOMuNlNd23DAMRC9F9b5N3uJuJy6JnT7//2l5oAq1u+fYfBJwrkTMABD68OA9v3Ybm1SJ3XSvZxGzWDKd5cKcIEEIkPzbX+2LG4AwmCMmBr5EWZ+8uPwKxMZD8jVsL3PvfvNewTofEVNAeHGkY2WhMAMSw216ovWlgLhHAggfXDptoiiqVy7ISwgkIROyPTjguZd87b75/RuhkdACIveaHW2xLtHAQkK3VL8lSRG0dSo1LdSOCVlKZHDl4hYaZyOULnUHmejgUZJ0gJte74g8QCc2VIMTQe95BIWG8jbC8nZkyP14o/SEFQm7OfHMeI8Uk4iWax9ILXiZv7QnXymgvZ/CmOS4lgO0By9+wvqKJKmexEyK6sGXQW/jVwad726P1B7i3NWS6mUapCDwJ8f1yOu0Q1YTMXTahGzHqSx9fpwXBRCmZ5Bp6mazG5XloChfT7PrNiA7auafH/4GnNujn/M9+sw2Tjv9ax/v12d3+lN/BknK6p1NqsTu6lnt/wGCb6Ns5HAfXwAAAABJRU5ErkJggg==);
                    background-size: 13px 13px;
                    vertical-align: -2px;
                }

        .donasteps {
            padding-bottom: 18px;
        }

            .donasteps .ds_wrap {
                width: 100%;
            }

                .donasteps .ds_wrap img {
                    width: 100%;
                    vertical-align: bottom;
                }

        .group_list .list_item .main {
            padding-left: 10px;
        }

        .lovers_list .list_item .main .m_name {
            width: 100%;
            position: relative;
            padding-right: 55%;
            text-overflow: ellipsis;
            overflow: hidden;
            white-space: nowrap;
            box-sizing: border-box;
        }

            .lovers_list .list_item .main .m_name .location {
                position: absolute;
                top: 2px;
                left: 60%;
                color: #88746c;
                font-size: 13px;
                font-weight: 400;
            }

                .lovers_list .list_item .main .m_name .location:before {
                    width: 10px;
                    height: 12px;
                    background-image: url(data:data/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAYCAMAAADNlS1EAAAAh1BMVEWgi4KhjYTw7Ov9/f3+/v6nlIyplY3p5OLXzsrSyMTm4d749va9rqiql4/i3NnTycX49vX59/ekkIjFuLKzopu/sKr+/f3j3dvCtK729PPj3dqyoZq1pJ3u6+munJT8/Pz9/Pz7+vn49/awnpfg2tfa0s+plo7Cta+vnZbJvbfVzMjJvbj///8A9O/AAAAALXRSTlP//////////////////////////////////////////////////////////wCl7wv9AAAAwklEQVQoz23RR3LEMBAEwQJo1nvvnbzq/+/TgRCXDKkvQORppgf/Celdx/HrOK6bmHUDAKGb1Tgd8ZvRNOGt4JniVuERICy+FgHgqOgJYHVRLyuAk+g38JGrmj+AT9E50EmTdYC5KMAs4QxANACDhAMgiA6BTcINMBTtA2VWbVYCfdH8DTjs1N0BKHNRIwDn9zMAMe0+aaw5qVta1rZ8VncPycK9UfI+4b7V/BaAbfsc1wIorm00VtO00R49/6Av9e8HHjQwTpeyETIAAAAASUVORK5CYII=);
                    background-size: 10px 12px;
                    vertical-align: -1px;
                }

        .swiper-container-horizontal > .swiper-pagination-bullets, .swiper-pagination-custom, .swiper-pagination-fraction {
            bottom: 6px;
        }

        .page1 .arrow, .video_wrap .video_close {
            bottom: 20px;
            background-position: center center;
            background-repeat: no-repeat;
        }

        .swiper-container-horizontal > .swiper-pagination-bullets .swiper-pagination-bullet {
            margin: 0 3px;
        }

        .swiper-pagination-bullet {
            width: 6px;
            height: 6px;
        }

        .swiper-pagination-bullet-active {
            background-color: #e02513;
        }

        .dialog_wrap {
            display: none;
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0,0,0,.7);
            z-index: 10;
        }

            .dialog_wrap .dialog {
                position: absolute;
                top: 50%;
                left: 50%;
                width: 300px;
                padding: 20px 15px;
                -webkit-transform: translate(-50%,-50%);
                transform: translate(-50%,-50%);
                background-color: #FFF;
                border-radius: 3px;
                box-sizing: border-box;
            }

                .dialog_wrap .dialog .btn_close {
                    display: block;
                    position: absolute;
                    right: 0;
                    top: 0;
                    width: 40px;
                    height: 40px;
                    background-image: url(data:data/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAQAAABKfvVzAAAAmklEQVQ4y5XUOw7EIAwAUTe0Cxw7B6GeY+RaKbKInw2O0kXzJLCEBSEQEcf3IyBC4KKQjnmicBGESOE+kvSvoiDkI6l5Rt4fe9LlFezIkDdgkSnvgUaWfAQzUfIZ9CTpt1pnUokxBG2MLzHGbM/dDdK3I7WrZs+lx8moxM4NsstVss8VcsoXcs4n4n2iuT1R7xLIdQn410wkIA8CmOPTRGVMbwAAAABJRU5ErkJggg==);
                    background-position: center center;
                    background-repeat: no-repeat;
                    background-size: 12px;
                }

        .page1, body {
            position: relative;
            overflow: hidden;
        }

            .page, .page1, .page1 .page1Bg {
                width: 100%;
            }

        .dialog_wrap .dialog h2 {
            padding-top: 20px;
            text-align: center;
        }

        .dialog_wrap .dialog ol {
            margin: 30px 0 30px 20px;
        }

            .dialog_wrap .dialog ol li {
                margin-bottom: 10px;
                line-height: 1.4;
            }

        @-webkit-keyframes a-marquee-ngo {
            0% {
                -webkit-transform: translate3d(0,0,0);
                transform: translate3d(0,0,0);
            }

            100% {
                -webkit-transform: translate3d(0,-100%,0);
                transform: translate3d(0,-100%,0);
            }
        }

        body {
            overflow-x: hidden;
        }

        .page {
            overflow: hidden;
        }

            .page.trans1 {
                -webkit-animation: a-page-trans1 .3s ease-out none;
                animation: a-page-trans1 .3s ease-out none;
            }

            .page.trans2 {
                -webkit-animation: a-page-trans2 .3s ease-out both;
                animation: a-page-trans2 .3s ease-out both;
            }

        .page1 {
            height: 100%;
        }

            .page1 .arrow {
                position: absolute;
                left: 50%;
                width: 23px;
                height: 11px;
                margin-left: -11px;
                background-image: url(data:data/png;base64,iVBORw0KGgoAAAANSUhEUgAAAC4AAAAWCAYAAAC/kK73AAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyRpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMy1jMDExIDY2LjE0NTY2MSwgMjAxMi8wMi8wNi0xNDo1NjoyNyAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNiAoTWFjaW50b3NoKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDo0RDI5NzVFNjRDQTkxMUU2ODYxMTkzODEzQzQ1M0RBQyIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDo0RDI5NzVFNzRDQTkxMUU2ODYxMTkzODEzQzQ1M0RBQyI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOjREMjk3NUU0NENBOTExRTY4NjExOTM4MTNDNDUzREFDIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOjREMjk3NUU1NENBOTExRTY4NjExOTM4MTNDNDUzREFDIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+5O5BcQAAAbZJREFUeNrUly9IA2EYxu/2x92QaRgLyopoVcSwpMWwalpYMZgsYhDbkoIgWgyDJYtlccWyZhJsCwZBDAbRICIGERmvz6fP4THcdv++8/bBD8a3933eH9vtu5spIsYorgSwwDFfj4Lv0bczPvGG/KxLYKlvIKZYdFSroTYS4JQbD6AQQ+kC3YSuCeebW3zjAyzFSHqRTmpt2/u9Raugy6JqDKSrdOnSzegnrpgBL2w4BOY/CKuZB3R4pZMxTFyRA9dsPAfpCKXTnCl0yP1VNyggBVoMuAGTEUhPcJZwdqpfrZuvbI9Bb2BOo/QsLwu19oddom5DK/K7yhqky478ipseL+Hz4J3huyFK7zBTZS+47fM6JA/uOegMJAMIJ5khzMx76fczMAMuOPAKZH1kZNkrzMp4zfD7aanHhDoHP4FpD71T4JG9dWYZUYnbbFLgE5Rc1JdYK+z1PTuMH9ey4zFhY0DduuP2vRJ0blgnQxE8U+yk5ww2uSesKYYxM8yzeBx0KNgGY7x9t7nXYY0RN3H7iGtS9A7c8nUz4NGpXdy+NGqOO2FNxxOmqfHP8howQUtH+JcAAwAWh5xz8SASOwAAAABJRU5ErkJggg==);
                background-size: 23px 11px;
                -webkit-animation: a-arrow 1s infinite alternate;
                animation: a-arrow 1s infinite alternate;
            }


        .page2, .video_wrap {
            width: 100%;
            overflow: hidden;
        }

        .page2 {
            opacity: 0;
            background-color: #eee;
        }

        .video_wrap {
            display: none;
            position: absolute;
            top: 0;
            left: 0;
            height: 100%;
            background-color: rgba(0,0,0,.8);
            z-index: 100;
        }

        .logo99, .slogn {
            overflow: visible;
        }

        .video_wrap .video_content {
            position: absolute;
            left: 0;
            top: 50%;
            background-color: #000;
            -webkit-transform: translate(0,-50%);
            transform: translate(0,-50%);
        }

        .head, .logo99, .logo99 .logo99_inner {
            top: 0;
            position: absolute;
        }

        .video_wrap .video_close {
            position: absolute;
            left: 50%;
            width: 40px;
            height: 40px;
            margin-left: -20px;
            background-image: url(data:data/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAQAAABKfvVzAAAAmklEQVQ4y5XUOw7EIAwAUTe0Cxw7B6GeY+RaKbKInw2O0kXzJLCEBSEQEcf3IyBC4KKQjnmicBGESOE+kvSvoiDkI6l5Rt4fe9LlFezIkDdgkSnvgUaWfAQzUfIZ9CTpt1pnUokxBG2MLzHGbM/dDdK3I7WrZs+lx8moxM4NsstVss8VcsoXcs4n4n2iuT1R7xLIdQn410wkIA8CmOPTRGVMbwAAAABJRU5ErkJggg==);
            background-size: 16px 16px;
            border: 1px solid #c6c6c6;
            border-radius: 100%;
        }

        .head {
            left: 0;
            width: 100%;
            height: 46px;
            background-color: #FFF;
            -webkit-transform: translate3d(0,-100%,0);
            transform: translate3d(0,-100%,0);
        }

            .head .top_btn {
                display: block;
                padding: 4px 8px;
                margin: 12px 12px 0;
                font-size: 12px;
                color: #50443f;
                line-height: 1;
                border: 1px solid #b3b2b2;
                border-radius: 30px;
            }

        .logo99, .slogn, .videolink {
            display: none;
        }

        .head .top_btn.ft {
            float: left;
        }

        .head .top_btn.rt {
            float: right;
        }

        .head.trans1 {
            -webkit-transform: translate3d(0,0,0);
            transform: translate3d(0,0,0);
            -webkit-animation: a-head-trans1 .3s ease-out none;
            animation: a-head-trans1 .3s ease-out none;
        }

        .head.trans2 {
            -webkit-animation: a-head-trans2 .3s ease-out both;
            animation: a-head-trans2 .3s ease-out both;
        }

        .logo99 {
            left: 50%;
            width: 168px;
            height: 44px;
            margin-left: -84px;
            -webkit-transform-origin: center center;
            transform-origin: center center;
        }

            .logo99 .logo99_inner {
                left: 0;
                height: 100%;
                width: 100%;
                background: url(http://mat1.gtimg.com/gongyi/m/wx/201699/logo99.png) no-repeat;
                background-size: 168px 88px;
            }

            .logo99 .logo99_mini {
                width: 100%;
                height: 100%;
                background: url(http://mat1.gtimg.com/gongyi/m/wx/201699/logo99.png) 0 -44px no-repeat;
                background-size: 168px 88px;
                opacity: 0;
            }

            .logo99.trans1 {
                -webkit-transform: translate3d(0,0,0) scale(.5);
                transform: translate3d(0,0,0) scale(.5);
                -webkit-animation: a-logo-trans1 .3s ease-out none;
                animation: a-logo-trans1 .3s ease-out none;
            }

                .logo99.trans1 .logo99_mini {
                    -webkit-animation: a-logo-exchange-trans1 .3s ease-in both;
                    animation: a-logo-exchange-trans1 .3s ease-in both;
                }

            .logo99.trans2 {
                -webkit-animation: a-logo-trans2 .3s ease-out both;
                animation: a-logo-trans2 .3s ease-out both;
            }

                .logo99.trans2 .logo99_mini {
                    -webkit-animation: a-logo-exchange-trans2 .3s ease-out both;
                    animation: a-logo-exchange-trans2 .3s ease-out both;
                }

        .slogn {
            position: absolute;
            top: 29%;
            left: 50%;
            width: 275px;
            height: 22px;
            margin-left: -138px;
            color: transparent;
        }

            .slogn .slogn_inner, .videolink {
                position: absolute;
                width: 100%;
                left: 0;
            }

            .slogn .slogn_inner {
                top: 0;
                height: 100%;
                background: url(http://mat1.gtimg.com/gongyi/m/wx/201699/slogn.png) no-repeat;
                background-size: 275px 22px;
            }

            .slogn.trans1 {
                -webkit-transform: translate3d(0,-50px,0) scale(0);
                transform: translate3d(0,-50px,0) scale(0);
                opacity: 0;
                -webkit-animation: a-slogn-trans1 .3s ease-out none;
                animation: a-slogn-trans1 .3s ease-out none;
            }

            .slogn.trans2 {
                -webkit-animation: a-slogn-trans2 .3s ease-in both;
                animation: a-slogn-trans2 .3s ease-in both;
            }

        .videolink {
            top: 29%;
            margin-top: 77px;
        }

            .videolink a {
                position: relative;
                color: #FFF;
            }

                .videolink a:after {
                    content: " ";
                    display: inline-block;
                    margin-left: 3px;
                    width: 13px;
                    height: 13px;
                    background-image: url(data:data/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAQAAACROWYpAAABp0lEQVRIx6XVPWsUURTG8bvJLgkSMWICCikshaAixBAbGwVZ/AJCFGuLoIK1KQRRhIAgWBkkgghpRAQ/gAQ/gFhoITJImiAhRapl+VnMrJnceQXnlPf+Z+ac8zznBCGKjvPWDeSfobcWdOK7MbroJ9j1wi19fcvW7IBtl6vhng3w3Fzhf056DN6bKIOn/MBnswVwFNM+4bfjMTwpwWoxryhWsGMqD3ds4V4DmMZNfDV2AN/Beis0CJ5hdQQfNbSrFzWsGh7zC7Mp/BBXI3Tf9Rr8Il4KwbiBvTSHf9EFmyYr4I4EE8E8HkSH3UxZexYq8Nu4FNzFmQoY1nRL4Dk8CjZxpAYmcboA9/Al+E6UcQzD/UL92Q8SCgdFmCfRnSFtv5w4Vfbl5pzLipbl3FTt8nZl1a7rc7VQsj5XK2xQI9FMYam2rxw6GseW6WZtp676E7nqXI2vDrkq9fOr1n5+mvfzaJKstJ4k3/KT5L9mWH56zlSCx3zAdnF6pqp50zi3P+b7XrcxlvVdc6PdxhgV74LXhV31zmLzrjp4xQnzliw5a6bguiz+Ag+LWdE60LmsAAAAAElFTkSuQmCC);
                    background-size: 13px 13px;
                    background-position: right top;
                    background-repeat: no-repeat;
                    vertical-align: -2px;
                }

        .statistics_bg, .statistics_col {
            display: none;
            position: absolute;
        }

        .videolink.trans1 {
            -webkit-transform: translate3d(0,-50px,0) scale(0);
            transform: translate3d(0,-50px,0) scale(0);
            opacity: 0;
            -webkit-animation: a-slogn-trans1 .3s ease-out none;
            animation: a-slogn-trans1 .3s ease-out none;
        }

        .videolink.trans2 {
            -webkit-animation: a-slogn-trans2 .3s ease-in both;
            animation: a-slogn-trans2 .3s ease-in both;
        }

        .statistics_bg {
            top: 0;
            left: 0;
            width: 100%;
            height: 87px;
            background-color: #C72327;
            opacity: .8;
            border-radius: 2px;
            -webkit-transform: translate3d(0,46px,0) scale(1,1);
            transform: translate3d(0,46px,0) scale(1,1);
            -webkit-transform-origin: center top;
            transform-origin: center top;
        }

            .statistics_bg.start {
                -webkit-animation: a-stat-start 1s ease both;
                animation: a-stat-start 1s ease both;
            }

            .statistics_bg.trans1 {
                border-radius: 0;
                -webkit-animation: a-stat-trans1 .3s ease none;
                animation: a-stat-trans1 .3s ease none;
            }

            .statistics_bg.trans2 {
                border-radius: 2px;
                -webkit-animation: a-stat-trans2 .3s ease-out both;
                animation: a-stat-trans2 .3s ease-out both;
            }

        .statistics_col {
            top: 70px;
            width: 33.33%;
            height: 60px;
            color: #FFF;
        }

            .statistics_col .p_title {
                opacity: .6;
            }

            .statistics_col .p_data {
                display: inline-block;
                font-size: 12px;
                margin-top: 11px;
            }

                .statistics_col .p_data span {
                    font-size: 14px;
                    font-weight: 400;
                }

                .statistics_col .p_data em {
                    display: inline-block;
                    vertical-align: -1px;
                    font-style: normal;
                    font-size: 12px;
                    -webkit-transform: scale(.85);
                    transform: scale(.85);
                    -webkit-transform-origin: center top;
                    transform-origin: center top;
                }

            .statistics_col.col1 {
                left: 0;
                width: 100%;
                -webkit-transform: translate3d(0,0,0);
                transform: translate3d(0,0,0);
            }

                .statistics_col.col1.start {
                    -webkit-animation: a-fadein 1s ease both;
                    animation: a-fadein 1s ease both;
                }

                .statistics_col.col1.trans1 {
                    -webkit-animation: a-col1-trans1 .3s ease none;
                    animation: a-col1-trans1 .3s ease none;
                }

                .statistics_col.col1.trans2 {
                    -webkit-animation: a-col1-trans2 .3s ease-out both;
                    animation: a-col1-trans2 .3s ease-out both;
                }

            .statistics_col.col2 {
                left: 0;
                overflow: hidden;
            }

                .statistics_col.col2.start {
                    -webkit-animation: a-fadein 1s ease both;
                    animation: a-fadein 1s ease both;
                }

                .statistics_col.col2.trans1 {
                    -webkit-animation: a-col23-trans1 .3s ease none;
                    animation: a-col23-trans1 .3s ease none;
                }

                    .statistics_col.col2.trans1 .col23_inner {
                        -webkit-animation: a-col2in-trans1 .3s ease-out none;
                        animation: a-col2in-trans1 .3s ease-out none;
                    }

                .statistics_col.col2.trans2 {
                    -webkit-animation: a-col23-trans2 .3s ease-out both;
                    animation: a-col23-trans2 .3s ease-out both;
                }

                    .statistics_col.col2.trans2 .col23_inner {
                        -webkit-animation: a-col2in-trans2 .3s ease-out both;
                        animation: a-col2in-trans2 .3s ease-out both;
                    }

            .statistics_col.col3 {
                right: 0;
                overflow: hidden;
            }

                .statistics_col.col3.start {
                    -webkit-animation: a-fadein 1s ease both;
                    animation: a-fadein 1s ease both;
                }

                .statistics_col.col3.trans1 {
                    -webkit-animation: a-col23-trans1 .3s ease none;
                    animation: a-col23-trans1 .3s ease none;
                }

                    .statistics_col.col3.trans1 .col23_inner {
                        -webkit-animation: a-col3in-trans1 .3s ease-out none;
                        animation: a-col3in-trans1 .3s ease-out none;
                    }

                .statistics_col.col3.trans2 {
                    -webkit-animation: a-col23-trans2 .3s ease-out both;
                    animation: a-col23-trans2 .3s ease-out both;
                }

                    .statistics_col.col3.trans2 .col23_inner {
                        -webkit-animation: a-col3in-trans2 .3s ease-out both;
                        animation: a-col3in-trans2 .3s ease-out both;
                    }

            .statistics_col.init.col1 .p_data {
                -webkit-transform: scale(1.71);
                transform: scale(1.71);
            }

            .statistics_col.init.col1.trans1 .p_data {
                -webkit-transform: scale(1);
                transform: scale(1);
                -webkit-animation: a-col1num-trans1 .3s ease none;
                animation: a-col1num-trans1 .3s ease none;
            }

            .statistics_col.init.col1.trans2 .p_data {
                -webkit-animation: a-col1num-trans2 .3s ease-out both;
                animation: a-col1num-trans2 .3s ease-out both;
            }

            .statistics_col.init.col2, .statistics_col.init.col3 {
                width: 50%;
            }

                .statistics_col.init.col2 .p_data, .statistics_col.init.col3 .p_data {
                    -webkit-transform: scale(1.28);
                    transform: scale(1.28);
                }

                .statistics_col.init.col2.trans1 .p_data, .statistics_col.init.col3.trans1 .p_data {
                    -webkit-transform: scale(1);
                    transform: scale(1);
                    -webkit-animation: a-col23num-trans1 .3s ease none;
                    animation: a-col23num-trans1 .3s ease none;
                }

                .statistics_col.init.col2.trans2 .p_data, .statistics_col.init.col3.trans2 .p_data {
                    -webkit-animation: a-col23num-trans2 .3s ease-out both;
                    animation: a-col23num-trans2 .3s ease-out both;
                }

            .statistics_col, .statistics_col .p_data span {
                -webkit-transform-origin: center top;
                transform-origin: center top;
            }

        .statistics_sep {
            position: absolute;
            top: 0;
            left: 50%;
            width: 94%;
            height: 1px;
            opacity: 0;
        }

            .statistics_sep:after, .statistics_sep:before {
                content: " ";
                position: absolute;
                display: block;
                width: auto;
                border-top: 1px solid rgba(0,0,0,.2);
                -webkit-transform-origin: center bottom;
                transform-origin: center bottom;
                -webkit-transform: scaleY(.5);
                transform: scaleY(.5);
            }

            .statistics_sep:before {
                left: 24px;
                right: 50%;
                margin-right: 10px;
            }

            .statistics_sep:after {
                right: 24px;
                left: 50%;
                margin-left: 10px;
            }

            .statistics_sep em {
                position: absolute;
                display: block;
                left: 50%;
                top: 50%;
                width: 4px;
                height: 4px;
                border: 1px solid rgba(0,0,0,.2);
                -webkit-transform: translate(-50%,-50%) rotateZ(45deg);
                transform: translate(-50%,-50%) rotateZ(45deg);
                -webkit-transform-origin: center center;
                transform-origin: center center;
            }

            .statistics_sep.ing {
                margin-top: 10px;
            }

            .statistics_sep.start {
                display: block;
                -webkit-animation: a-fadein 1s ease both;
                animation: a-fadein 1s ease both;
            }

            .statistics_sep.trans1 {
                display: none;
            }

            .statistics_sep.trans2 {
                display: block;
                -webkit-animation: a-fadein .3s ease-out both .3s;
                animation: a-fadein .3s ease-out both .3s;
            }

        .countdown {
            display: none;
            position: absolute;
            top: 60px;
            left: 0;
            width: 100%;
            color: #FFF;
        }

            .countdown .t_title {
                font-size: 16px;
                margin-bottom: 12px;
            }

            .countdown .t_time {
                -webkit-transform-origin: center top;
                transform-origin: center top;
            }

            .countdown .t_block {
                position: relative;
                display: inline-block;
                margin: 0 4px;
            }

                .countdown .t_block .t_tt {
                    overflow: hidden;
                }

                    .countdown .t_block .t_tt span {
                        display: block;
                        float: left;
                        margin: 0 1px;
                        padding: 6px 0;
                        width: 18px;
                        font-size: 16px;
                        background-color: rgba(65,0,2,.36);
                        border-radius: 2px;
                    }

                .countdown .t_block:nth-child(1):after, .countdown .t_block:nth-child(1):before, .countdown .t_block:nth-child(2):after, .countdown .t_block:nth-child(2):before {
                    content: " ";
                    display: block;
                    position: absolute;
                    top: 50%;
                    right: -7px;
                    margin-top: -11px;
                    width: 2px;
                    height: 2px;
                    background-color: rgba(0,0,0,.26);
                    -webkit-transform: scale(1.2);
                    transform: scale(1.2);
                    -webkit-transform-origin: center center;
                    transform-origin: center center;
                }

                .countdown .t_block:nth-child(1):after, .countdown .t_block:nth-child(2):after {
                    margin-top: -2px;
                }

            .countdown .t_type {
                margin-top: 0;
                font-size: 12px;
                color: rgba(0,0,0,.3);
                -webkit-transform: scale(.7);
                transform: scale(.7);
                -webkit-transform-origin: center bottom;
                transform-origin: center bottom;
            }

            .countdown.start .t_time, .countdown.start .t_title {
                -webkit-animation: a-fadein 1s ease both;
                animation: a-fadein 1s ease both;
            }

            .countdown.trans1 .t_title {
                -webkit-animation: a-counttitle-trans1 .3s ease none;
                animation: a-counttitle-trans1 .3s ease none;
            }

            .countdown.trans1 .t_time {
                -webkit-animation: a-counttime-trans1 .3s ease none;
                animation: a-counttime-trans1 .3s ease none;
            }

            .countdown.trans2 .t_title {
                -webkit-animation: a-counttitle-trans2 .3s ease-out both;
                animation: a-counttitle-trans2 .3s ease-out both;
            }

            .countdown.trans2 .t_time {
                -webkit-animation: a-counttime-trans2 .3s ease-out both;
                animation: a-counttime-trans2 .3s ease-out both;
            }

            .countdown.init .t_title {
                -webkit-transform: scale(1.1);
                transform: scale(1.1);
            }

            .countdown.init .t_time {
                -webkit-transform: scale(1.2);
                transform: scale(1.2);
            }

            .countdown.init.trans1 .t_time, .countdown.init.trans1 .t_title {
                -webkit-transform: translate3d(0,0,0) scale(1);
                transform: translate3d(0,0,0) scale(1);
            }

        .logo99.start .logo99_inner, .slogn.start .slogn_inner {
            -webkit-animation: a-logo-start 1s ease both;
            animation: a-logo-start 1s ease both;
        }

        .videolink.start {
            -webkit-animation: a-logo-exchange-trans1 .6s linear both .4s;
            animation: a-logo-exchange-trans1 .6s linear both .4s;
        }

        @media screen and (min-width:375px) and (max-width:410px) {
            .statistics_col .p_data {
                margin-top: 10px;
            }

                .statistics_col .p_data span {
                    font-size: 16px;
                }
        }

        @media screen and (min-width:410px) {
            .statistics_col .p_data {
                margin-top: 9px;
            }

                .statistics_col .p_data span {
                    font-size: 18px;
                }
        }

        @-webkit-keyframes a-arrow {
            0% {
                -webkit-transform: translate3d(0,-6px,0);
                transform: translate3d(0,-6px,0);
            }

            100% {
                -webkit-transform: translate3d(0,6px,0);
                transform: translate3d(0,6px,0);
            }
        }

        @-webkit-keyframes a-logo-exchange-trans1 {
            0% {
                opacity: 0;
            }

            100% {
                opacity: 1;
            }
        }

        @-webkit-keyframes a-logo-exchange-trans2 {
            0% {
                opacity: 1;
            }

            100% {
                opacity: 0;
            }
        }

        @-webkit-keyframes a-slogn-trans1 {
            0% {
                -webkit-transform: translate3d(0,0,0) scale(1);
                transform: translate3d(0,0,0) scale(1);
                opacity: 1;
            }

            100% {
                -webkit-transform: translate3d(0,-50px,0) scale(0);
                transform: translate3d(0,-50px,0) scale(0);
                opacity: 0;
            }
        }

        @-webkit-keyframes a-slogn-trans2 {
            0% {
                -webkit-transform: translate3d(0,-50px,0) scale(0);
                transform: translate3d(0,-50px,0) scale(0);
                opacity: 0;
            }

            100% {
                -webkit-transform: translate3d(0,0,0) scale(1);
                transform: translate3d(0,0,0) scale(1);
                opacity: 1;
            }
        }

        @-webkit-keyframes a-head-trans1 {
            0% {
                -webkit-transform: translate3d(0,-46px,0);
                transform: translate3d(0,-46px,0);
            }

            100% {
                -webkit-transform: translate3d(0,0,0);
                transform: translate3d(0,0,0);
            }
        }

        @-webkit-keyframes a-head-trans2 {
            0% {
                -webkit-transform: translate3d(0,0,0);
                transform: translate3d(0,0,0);
            }

            100% {
                -webkit-transform: translate3d(0,-46px,0);
                transform: translate3d(0,-46px,0);
            }
        }

        @-webkit-keyframes a-start {
            0% {
                margin-top: -100px;
                opacity: 0;
            }

            100% {
                margin-top: 0;
                opacity: 1;
            }
        }

        @-webkit-keyframes a-logo-start {
            0% {
                opacity: 0;
                -webkit-transform: translateY(-20px);
                transform: translateY(-20px);
            }

            100% {
                opacity: 1;
                -webkit-transform: translateY(0);
                transform: translateY(0);
            }
        }

        @-webkit-keyframes a-slogn-start {
            0% {
                opacity: 0;
            }

            100% {
                opacity: 1;
            }
        }

        @-webkit-keyframes a-col1num-trans1 {
            0% {
                -webkit-transform: scale(1.71);
                transform: scale(1.71);
            }

            100% {
                -webkit-transform: scale(1);
                transform: scale(1);
            }
        }

        @-webkit-keyframes a-col1num-trans2 {
            0% {
                -webkit-transform: scale(1);
                transform: scale(1);
            }

            100% {
                -webkit-transform: scale(1.71);
                transform: scale(1.71);
            }
        }

        @-webkit-keyframes a-col23num-trans1 {
            0% {
                -webkit-transform: scale(1.28);
                transform: scale(1.28);
            }

            100% {
                -webkit-transform: scale(1);
                transform: scale(1);
            }
        }

        @-webkit-keyframes a-col23num-trans2 {
            0% {
                -webkit-transform: scale(1);
                transform: scale(1);
            }

            100% {
                -webkit-transform: scale(1.28);
                transform: scale(1.28);
            }
        }

        @-webkit-keyframes a-fadein {
            0% {
                opacity: 0;
            }

            100% {
                opacity: 1;
            }
        }

        @-webkit-keyframes a-stat-start {
            0% {
                opacity: 0;
            }

            100% {
                opacity: .8;
            }
        }

        .mod_QQmoney {
            padding: 10px 0;
        }

            .mod_QQmoney img {
                margin-bottom: 10px;
                height: 51px;
                vertical-align: top;
            }

        .mod.mod_end_info {
            margin-top: 0;
            padding: 24px 0 20px;
            color: #50443f;
            font-size: 14px;
            line-height: 20px;
            font-weight: 600;
        }

        .steps_end_info .match_info {
            padding: 21px 0 23px;
        }

        .steps_end_info .steps_title {
            margin-bottom: 16px;
            color: #e02513;
            font-size: 15px;
        }

        .steps_end_info p {
            line-height: 19px;
            font-weight: 500;
            font-size: 14px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div class="page trans1" id="container" style="display: block;">
            <div class="page2" style="opacity: 1;">
                <div class="mod">
                    <div class="mod_title">
                        <h2>爱心配捐</h2>
                        <a href="javascript:;" class="a_match_rule" id="rule1">活动规则</a>
                    </div>
                    <div class="match_info">
                        <p class="match_p1"><span class="num"></span>今日总配捐池<span class="num">200kg</span></p>
                        <p class="match_p2">已经被使用配捐额<span class="num">30.68kg</span></p>
                    </div>
                </div>
            </div>
        </div>



        <!-- 规则1 -->
        <div class="dialog_wrap" id="dialog1">
            <div class="dialog">
                <div class="swiper-container ruleSlider" id="ruleSlider">
                    <div class="swiper-wrapper">
                        <div class="swiper-slide">
                            <h2>爱心配捐规则</h2>
                            <ol>
                                <li>配捐时段：每天早上10点开始，先捐先配，配完即止。</li>
                                <li>项目范围：所有在筹项目。 </li>
                                <li>配捐额：看详细配捐池。</li>
                                <li>配捐规则：前50%配捐池进行6：1配捐，最低10元起配，后50%随机配捐，用完截止。</li>
                            </ol>
                        </div>
                    </div>
                    <div class="swiper-pagination"></div>
                </div>
                <div class="btn_close"></div>
            </div>
        </div>

    </form>
</body>
</html>
<script src="js/jquery.min.js">

   
</script>
<script>
    $("#dialog1").show();
    $(".btn_close").click(function () {
        $("#dialog1").hide();
    });
</script>
