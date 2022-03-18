import { Variant } from "./variant.js";
import { LoadoutView } from "./LoadoutView.js";

export class Hunt{

    constructor(listOfGuns, listofSecondGuns, listOfTools){

        this.listOfGuns = listOfGuns  
        this.listofSecondGuns = listofSecondGuns
        this.listOfTools = listOfTools
    }

    crtajFormu(host){

        this.loadoutView = new LoadoutView()
        this.loadoutView.crtajLoadout(host)

        let arrayLabels =["Loadout Name","Gun", "Gun Variant", "Secondery Gun" ]

        let forma= document.createElement("div");
        forma.className="forma";
        host.appendChild(forma);

        let configer=document.createElement("div");
        configer.className="conf";
        forma.appendChild(configer)

        let divLabels = document.createElement("div");
        divLabels.classList.add("labels");
        configer.appendChild(divLabels);
    
    
        let divFields = document.createElement("div");
        divFields.classList.add("fields");
        configer.appendChild(divFields);
    
        let l;
        let f;

        arrayLabels.forEach(x =>{
            l = document.createElement("label");
            l.className="lable"
            l.innerHTML=x;
            divLabels.appendChild(l);
    
        });
    
    
        //loadout name
        f=document.createElement("input");
        f.className="field loadaoutName"
        divFields.appendChild(f);


        this.crtajGuns(divFields)
        this.crtajSecondGuns(divFields)

        let toolLable = document.createElement("label")
        toolLable.className="toolLable"
        toolLable.innerText="Tools :"

        forma.appendChild(toolLable)

        let toolsContainer = document.createElement("div")
        toolsContainer.className="toolContainer"

        forma.appendChild(toolsContainer)
        let ul = document.createElement("ul")
        ul.className="cboxTags"
        toolsContainer.appendChild(ul)


        let i = 0;
        this.listOfTools.forEach(el =>{
            let li = document.createElement("li")
            ul.appendChild(li)
            let input = document.createElement("input")
            input.type="checkbox"
            let id = el.toolName.replace(/\s/g, '')
            input.setAttribute("id",id)
            input.value=el.toolName
            input.addEventListener("change", () => {
                this.loadoutView.updateTool(input.value)
            })
            let lable = document.createElement("label")
            lable.setAttribute("for",id)
            lable.innerText=el.toolName
            li.appendChild(input)
            li.appendChild(lable)
            
        })

        let loadoutDiv = document.createElement("div")
        loadoutDiv.className="loadoutPicker"
        forma.appendChild(loadoutDiv)
        let count = document.createElement("h2")
        count.className="count"
        count.innerText="1/5"
        loadoutDiv.appendChild(count)
        
    }

    crtajGuns(host){


        let s=document.createElement("select");
        s.className="gunSelect field"
        
        let o
        this.listOfGuns.forEach(g=>
            {
                o = document.createElement("option");
                o.value=g.gunName;
                o.innerHTML=g.gunName;
                s.appendChild(o);         
            })
            host.appendChild(s);
            let sv=document.createElement("select")
            sv.className="variantSelect field";

            sv.addEventListener("change" , ()=> {
                this.loadoutView.crtajGunInfo()
            })
            host.appendChild(sv);

            s.addEventListener("change", ()=>{
                let selected = document.querySelector(".gunSelect")
                
                this.crtajVariants(selected.value,host)
                this.loadoutView.crtajGunInfo()

            })
    }

    crtajVariants(gName,host){


        let variantNames = [];
        let o;
        
        let s = document.querySelector(".variantSelect")
        //ovaj deo resetuje selector svaki put kada se bira nova puska
        if(this.listOfVariants != null)
        {
            this.listOfVariants.forEach(g=>{

                o = document.querySelector(".options")
                
                s.removeChild(o);

            })
        }

        fetch("https://localhost:5001/GunsVariant/GetGunVariantByname/"+gName+"/",
        {
            method: "GET"
        })
        .then(p=>{
            if(p.ok)
            {
                p.json().then(variants=>{
                    variants.forEach(variant => {
                        var v = new Variant(variant.ime);
                        variantNames.push(v)
                    })

                    this.listOfVariants = variantNames;
                
                    //popunjavamo variant selector
                    this.listOfVariants.forEach(g=>
                        {
                            o = document.createElement("option");
                            o.className="options"
                            o.value=g.variantName;
                            o.innerHTML=g.variantName;
                            s.appendChild(o);         
                        })
                })
            }
    })


    }

    crtajSecondGuns(host)
    {
        let s=document.createElement("select");
        s.className="secondGunSelect field"
        
        let o
        this.listofSecondGuns.forEach(g=>
            {
                o = document.createElement("option");
                o.value=g.SGunName;
                o.innerHTML=g.SGunName;
                s.appendChild(o);         
            })
            host.appendChild(s);

            s.addEventListener("change", ()=>{
                this.loadoutView.crtajSecondGunInfo()
            })        
    }

}

