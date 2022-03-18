import { Gun } from "./gun.js";
import { Hunt } from "./hunt.js";
import { SecondGun } from "./secondGun.js"
import { Tool } from "./tool.js"

var gunNames = [];
var sGunNames = [];
var toolsNames = [];

fetch("https://localhost:5001/Guns/GetGuns")
.then(p=>{
        p.json().then(guns=>{
            guns.forEach(gun => {
                var g = new Gun(gun);
                gunNames.push(g)
            })
            fetch("https://localhost:5001/SecondGun/GetSecondGunsNames")
            .then(p=>{
                p.json().then(secondGuns=>{
                    secondGuns.forEach(sGun => {
                        var sg = new SecondGun(sGun);
                        sGunNames.push(sg)
                    })
                    fetch("https://localhost:5001/Tools/GetToolsNames")
                    .then(p=>{
                        p.json().then(toolS=>{
                            toolS.forEach(tool => {
                                var tool = new Tool(tool);
                                toolsNames.push(tool)
                            })
                            var h = new Hunt(gunNames, sGunNames, toolsNames);
                            h.crtajFormu(parent)
                        })
                    })

                })
            })

        })
    })

    var parent = document.querySelector("#parent");

    // let toolsContainer = document.createElement("div")
    // toolsContainer.className="container"
